VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.ocx"
Begin VB.UserControl WebSocketSvr 
   ClientHeight    =   2940
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   4830
   ScaleHeight     =   2940
   ScaleWidth      =   4830
   Begin MSWinsockLib.Winsock PageSock 
      Index           =   0
      Left            =   4230
      Top             =   810
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.ListBox lstLog 
      Appearance      =   0  'Flat
      Height          =   2700
      IntegralHeight  =   0   'False
      Left            =   90
      TabIndex        =   0
      Top             =   90
      Width           =   4035
   End
   Begin MSWinsockLib.Winsock Sock 
      Index           =   0
      Left            =   4230
      Top             =   120
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
End
Attribute VB_Name = "WebSocketSvr"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit

Private Declare Function CryptBinaryToString Lib "Crypt32.dll" Alias "CryptBinaryToStringW" (ByRef pbBinary As Byte, ByVal cbBinary As Long, ByVal dwFlags As Long, ByVal pszString As Long, ByRef pcchString As Long) As Long
Private Declare Function CryptStringToBinary Lib "Crypt32.dll" Alias "CryptStringToBinaryW" (ByVal pszString As Long, ByVal cchString As Long, ByVal dwFlags As Long, ByVal pbBinary As Long, ByRef pcbBinary As Long, ByRef pdwSkip As Long, ByRef pdwFlags As Long) As Long
Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (Destination As Any, Source As Any, ByVal length As Long)

Public Enum OpCodeEnum
    opContin = 0    '连续消息片断
    opText = 1      '文本消息片断
    opBinary = 2    '二进制消息片断  '3 - 7 非控制帧保留
    opClose = 8     '连接关闭
    opPing = 9      '心跳检查的ping
    opPong = 10     '心跳检查的pong  '11-15 控制帧保留
End Enum

Public Enum DataFrameMask
    FIN = &H80                              'Byte0 1:当前消息的最后一帧；0:有后续
    RSV1 = &H40: RSV2 = &H20: RSV3 = &H10   'Byte0.H, 1位，若没有自定义协议,必须为0,否则必须断开.
    opCode = &HF                            'Byte0.L, 4位操作码，定义有效负载数据，如果收到了一个未知的操作码，连接必须断开.
    mask = &H80                             'Byte1 传输的数据是否有加掩码
    Payloadlen = &H7F                       'Byte1.x 传输数据的长度，126:后续2字节为实际长度，127:后续8字节为实际长度
End Enum

Event Handshaked(ByVal idxFrom As Integer)
Event RecvData(ByVal idxFrom As Integer, req() As Byte, ByVal offset As Long)
Event RecvMsg(ByVal idxFrom As Integer, req As String)
Event GetStaticRes(ByVal idxFrom As Integer, ByVal URL As String, ByRef Res() As Byte)
Private utf8 As Object, SHA1 As Object
Private HTTP200 As String, HTTP404 As String

Public Property Get wsPort() As Long: wsPort = Sock(0).LocalPort: End Property
Public Property Let wsPort(ByVal v As Long): Sock(0).LocalPort = v:   PropertyChanged "wsPort": End Property
Public Property Get pgPort() As Long: pgPort = PageSock(0).LocalPort: End Property
Public Property Let pgPort(ByVal v As Long): PageSock(0).LocalPort = v:   PropertyChanged "pgPort": End Property
Public Property Get Listen() As Boolean: Listen = Sock(0).State = sckListening: End Property
Public Property Let Listen(ByVal v As Boolean): CallByName Sock(0), IIf(v, "Listen", "Close"), VbMethod: CallByName PageSock(0), IIf(v, "Listen", "Close"), VbMethod: End Property

Private Sub UserControl_Initialize()
    Set utf8 = CreateObject("System.Text.UTF8Encoding")
    Set SHA1 = CreateObject("System.Security.Cryptography.SHA1Managed")
    Sock(0).LocalPort = 8081: PageSock(0).LocalPort = 8080
    HTTP200 = Replace("HTTP/1.1 200 OK||Server: JiWebServerLite||Content-Length: ", "||", vbCrLf)
    HTTP404 = "<html><h1>Not Found</h1>JiWebServer: <br><br>The requested URL was not found on this server.</html>"
    HTTP404 = Replace("HTTP/1.1 404 ERROR||Server: JiWebServerLite||Content-Length: " & Len(HTTP404) & "||||" & HTTP404, "||", vbCrLf)
End Sub

Private Sub UserControl_Resize()
    On Error Resume Next: lstLog.Move 0, 0, ScaleWidth, ScaleHeight
End Sub

Private Sub Showlog(Log As String)
    lstLog.AddItem Now & vbTab & Log: If lstLog.ListCount > 1024 Then lstLog.RemoveItem 0
    lstLog.Selected(lstLog.ListCount - 1) = True
End Sub

Private Sub Sock_ConnectionRequest(Index As Integer, ByVal requestID As Long)
    Dim i As Long
    For i = 1 To Sock.UBound
        If Sock(i).State <> sckConnecting And Sock(i).State <> sckConnected Then Sock(i).Close: Exit For
    Next
    If i > Sock.UBound Then Load Sock(i)
    Sock(i).Accept requestID: Sock(i).Tag = ""
    Showlog i & "s" & Sock(i).RemoteHostIP & ":" & Sock(i).RemotePort & vbTab & "Request Connection."
End Sub

Private Sub Sock_DataArrival(Index As Integer, ByVal bytesTotal As Long)
    Dim buff() As Byte, opCode As Byte, isMask As Boolean, Payloadlen As Long, DataOffset As Long
    If Sock(Index).Tag = "" Then Handshake Index: Exit Sub

    Sock(Index).GetData buff
    opCode = buff(0) And DataFrameMask.opCode
    Payloadlen = buff(1) And DataFrameMask.Payloadlen: isMask = buff(1) And DataFrameMask.mask
    DataOffset = IIf(Payloadlen < 126, 2, IIf(Payloadlen = 126, 6, 10)) - isMask * 4
    If Payloadlen = 126 Then Payloadlen = buff(2) * 256 + buff(3) _
        Else: If Payloadlen = 127 Then Payloadlen = ((buff(2) * 256 + buff(3)) * 256 + buff(4)) * 256 + buff(5) ' may err

    Debug.Assert bytesTotal = DataOffset + Payloadlen      '完整接收,否则需要黏包

    If opCode = OpCodeEnum.opBinary Then
        If isMask Then MaskData buff, DataOffset, UBound(buff), buff, DataOffset - 4, 4
    ElseIf opCode = OpCodeEnum.opText Then
        ReDim strBuff(Payloadlen - 1) As Byte
        CopyMemory strBuff(0), buff(DataOffset), Payloadlen
        If isMask Then MaskData strBuff, 0, UBound(strBuff), buff, DataOffset - 4, 4
        Dim req As String: req = utf8.GetString(strBuff)
        RaiseEvent RecvMsg(Index, req)
        Showlog Index & "s" & Sock(Index).RemoteHostIP & ":" & Sock(Index).RemotePort & vbTab & "Recv Msg: " & req
    ElseIf opCode = OpCodeEnum.opPing Then
        Sock(Index).SendData Hex2Bin("8a00")
    ElseIf opCode = OpCodeEnum.opPong Then
        Sock(Index).SendData Hex2Bin("8900")
    Else: GoTo CLoseConnect
    End If
    Exit Sub

CLoseConnect:
    Sock(Index).Close
End Sub

Private Sub Sock_Close(Index As Integer)
    Showlog Index & "s" & Sock(Index).RemoteHostIP & ":" & Sock(Index).RemotePort & vbTab & "CLOSED"
End Sub

Private Sub Sock_Connect(Index As Integer)
    Showlog Index & "s" & Sock(Index).RemoteHostIP & ":" & Sock(Index).RemotePort & vbTab & "CONNECT"
End Sub

Private Sub Sock_Error(Index As Integer, ByVal Number As Integer, Description As String, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, CancelDisplay As Boolean)
    Showlog Index & "s" & Sock(Index).RemoteHostIP & ":" & Sock(Index).RemotePort & vbTab & "ERROR: " & Description & " Src: " & Source
End Sub

Public Function Handshake(ByVal Index As Integer)
    Const MagicKey = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
    Dim i As Long, AcceptKey As String, bytes() As Byte, req As String, Res As String

    Sock(Index).GetData req
    AcceptKey = strNameValue(req, "Sec-WebSocket-Key:")
    bytes = StrConv(AcceptKey & MagicKey, vbFromUnicode)
    bytes = SHA1.ComputeHash_2(bytes)
    AcceptKey = Base64Encode(bytes)

    Res = "HTTP/1.1 101 Web Socket Protocol Handshake||Upgrade: WebSocket||Connection: Upgrade||Sec-WebSocket-Accept:[Accept]||"
    Res = Replace(Res, "||", vbCrLf)
    Res = Replace(Res, "[Accept]", AcceptKey)
    Sock(Index).SendData Res
    Showlog Index & "s" & Sock(Index).RemoteHostIP & ":" & Sock(Index).RemotePort & vbTab & "Handshake."
    Sock(Index).Tag = 0
    RaiseEvent Handshaked(Index)
End Function

Private Function strNameValue(str As String, Name As String) As String
    Dim L0 As Long, L1 As Long
    L0 = InStr(str, Name): If L0 = 0 Then Exit Function Else L0 = L0 + Len(Name)
    L1 = InStr(L0 + Len(Name), str, vbCrLf): If L1 = 0 Then Exit Function
    strNameValue = Trim(Mid(str, L0, L1 - L0))
End Function

Private Function Base64Decode(sBase64Buf As String) As String
    Const CRYPT_STRING_BASE64 As Long = 1
    Dim bTmp() As Byte, lLen As Long, dwActualUsed As Long
    If CryptStringToBinary(StrPtr(sBase64Buf), Len(sBase64Buf), CRYPT_STRING_BASE64, StrPtr(vbNullString), lLen, 0&, dwActualUsed) = 0 Then Exit Function       'Get output buffer length
    ReDim bTmp(lLen - 1)
    If CryptStringToBinary(StrPtr(sBase64Buf), Len(sBase64Buf), CRYPT_STRING_BASE64, VarPtr(bTmp(0)), lLen, 0&, dwActualUsed) = 0 Then Exit Function 'Convert Base64 to binary.
    Base64Decode = StrConv(bTmp, vbUnicode)
End Function

Private Function Base64Encode(Data() As Byte) As String
    Const CRYPT_STRING_BASE64 As Long = 1
    Dim nLen As Long
    If CryptBinaryToString(Data(0), UBound(Data) + 1, CRYPT_STRING_BASE64, StrPtr(vbNullString), nLen) = 0 Then Exit Function  'Determine Base64 output String length required.
    Base64Encode = String(nLen - 1, vbNullChar)   'Convert binary to Base64.
    If CryptBinaryToString(Data(0), UBound(Data) + 1, CRYPT_STRING_BASE64, StrPtr(Base64Encode), nLen) = 0 Then Exit Function
End Function

Private Function Hex2Bin(strHex As String) As Byte()
    Dim i As Long
    ReDim Ret(Len(strHex) / 2 - 1) As Byte
    For i = 0 To UBound(Hex2Bin): Ret(i) = CByte("&h" & Mid(strHex, i * 2, 2)): Next
    Hex2Bin = Ret
End Function

Private Sub MaskData(buff() As Byte, bufFrom As Long, bufTo As Long, mask() As Byte, mask0 As Long, mskLen As Long)
    Dim i As Long, k As Long, L As Long
    For i = bufFrom To bufTo
        buff(i) = buff(i) Xor mask(mask0 + k): k = (k + 1) Mod mskLen
    Next
End Sub

Public Function SendMsg(ByVal Index As Integer, ByVal Res As String)
    SendData Index, (utf8.GetBytes_4(Res)), opText
End Function

Public Function SendData(ByVal Index As Integer, Res() As Byte, Optional ByVal opCode As OpCodeEnum = opBinary)
    Dim i As Long, nLen As Long, offset As Long
    nLen = UBound(Res) + 1: offset = IIf(nLen < &H7E, 2, 4)
    ReDim Preserve Res(UBound(Res) + offset)
    CopyMemory Res(offset), Res(0), nLen
    Res(0) = DataFrameMask.FIN + opCode
    If nLen < &H7E Then Res(1) = nLen Else Res(1) = &H7E: Res(2) = nLen \ 256: Res(3) = nLen Mod 256

    ' index=0 所有, index<0, 除-index都
    For i = IIf(Index > 0, Index, 1) To IIf(i > 0, Index, Sock.UBound):
        If i <> -Index And Sock(i).State = sckConnected Then Sock(i).SendData Res
    Next
End Function

Private Sub PageSock_ConnectionRequest(Index As Integer, ByVal requestID As Long)
    Dim i As Long
    For i = 1 To PageSock.UBound
        If PageSock(i).State <> sckConnecting And PageSock(i).State <> sckConnected Then PageSock(i).Close: Exit For
    Next
    If i > PageSock.UBound Then Load PageSock(i)
    PageSock(i).Accept requestID
End Sub

Private Sub PageSock_DataArrival(Index As Integer, ByVal bytesTotal As Long)
    Const mHeader = "HTTP/1.1 200 OK" & vbCrLf & "Server: JiWebServerLite" & vbCrLf & "Cache-Control: no-cache" & vbCrLf & "Connection: close" & vbCrLf & vbCrLf
    On Error Resume Next
    Dim i As Long, req As String, URL As String, strBuf As String
    PageSock(Index).GetData req
    i = InStr(req, " "): URL = UCase(Mid(req, i + 1, InStr(i + 1, req, " ") - i - 1))
    Showlog Index & "w" & PageSock(Index).RemoteHostIP & ":" & PageSock(Index).RemotePort & vbTab & "Web Get: " & URL
    ReDim Buf(0) As Byte: Buf = LoadResData(URL, "Page")
    RaiseEvent GetStaticRes(Index, URL, Buf)
    If UBound(Buf) > 0 Then
        PageSock(Index).SendData HTTP200 & (UBound(Buf) + 1) & vbCrLf & vbCrLf
        PageSock(Index).SendData Buf
    Else: PageSock(Index).SendData HTTP404
    End If
End Sub

Private Sub PageSock_Close(Index As Integer)
    Showlog Index & "w" & PageSock(Index).RemoteHostIP & ":" & PageSock(Index).RemotePort & vbTab & "CLOSED"
End Sub

Private Sub UserControl_ReadProperties(PropBag As PropertyBag)
    Sock(0).LocalPort = PropBag.ReadProperty("wsPort", 8081)
    PageSock(0).LocalPort = PropBag.ReadProperty("pgPort", 8080)
End Sub

Private Sub UserControl_WriteProperties(PropBag As PropertyBag)
    Call PropBag.WriteProperty("wsPort", Sock(0).LocalPort, 8081)
    Call PropBag.WriteProperty("pgPort", PageSock(0).LocalPort, 8080)
End Sub


