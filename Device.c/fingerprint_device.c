#include <stdio.h>
#include <string.h>
#include <windows.h>

#include "fingerprint_device.h"
HANDLE hComm;

void printData(U8Bit *data, U32Bit length) {
  for (int i = 0; i < length; i++)
    printf("%02x ", data[i]);
  printf("\t(%2d)\n", length);
}

S32Bit FP_device_open(const char *portname, int baudrate, char parity,
                      char databit, char stopbit, U32Bit timeout) {
  hComm = CreateFileA(portname, GENERIC_READ | GENERIC_WRITE, 0, NULL,
                      OPEN_EXISTING, 0, NULL);

  if ((HANDLE)-1 == hComm)
    return 0;

  DCB dcb;
  dcb.DCBlength = sizeof(dcb);
  if (!SetupComm(hComm, 1024, 1024) ||
      !GetCommState(hComm, &dcb)) { //配置缓冲区大小
    return 0;
  }
  // 配置参数
  dcb.BaudRate = baudrate; // 波特率
  dcb.ByteSize = databit;  // 数据位
  dcb.Parity = parity;
  dcb.StopBits = stopbit;
  if (!SetCommState(hComm, &dcb)) {
    return 0;
  }

  COMMTIMEOUTS TimeOuts;
  TimeOuts.ReadIntervalTimeout = timeout;
  TimeOuts.ReadTotalTimeoutMultiplier = timeout;
  TimeOuts.ReadTotalTimeoutConstant = timeout;
  TimeOuts.WriteTotalTimeoutMultiplier = timeout;
  TimeOuts.WriteTotalTimeoutConstant = timeout;
  SetCommTimeouts(hComm, &TimeOuts);
  PurgeComm(hComm, PURGE_TXCLEAR | PURGE_RXCLEAR);
  return 1;
}

/* read one byte */
S32Bit FP_device_read_one_byte(U8Bit *data, U32Bit timeout) {
  return FP_device_read_data(data, 1, timeout);
}

S32Bit FP_device_close() {
  if (hComm != INVALID_HANDLE_VALUE) CloseHandle(hComm);
  return 1;
}

/* read data */
S32Bit FP_device_read_data(U8Bit *data, U32Bit length, U32Bit timeout) {
  DWORD wCount = length; //成功读取的数据字节数
  BOOL bReadStat = ReadFile(hComm,
                            data,   //数据首地址
                            wCount, //要读取的数据最大字节数
                            &wCount, // DWORD*,用来接收返回成功读取的数据字节数
                            NULL); // NULL为同步发送，OVERLAPPED*为异步发送
  printData(data, wCount);
  return (bReadStat && wCount == length) ? FP_OK : FP_DEVICE_TIMEOUT_ERROR;
}

/* write data */
S32Bit FP_device_write_data(U8Bit *data, U32Bit length, U32Bit timeout) {
  DWORD dwBytesWrite = length; //成功写入的数据字节数
  BOOL bWriteStat =
      WriteFile(hComm,
                data,         //数据首地址
                dwBytesWrite, //要发送的数据字节数
                &dwBytesWrite, // DWORD*，用来接收返回成功发送的数据字节数
                NULL); // NULL为同步发送，OVERLAPPED*为异步发送
  printData(data, dwBytesWrite);
  return (bWriteStat && dwBytesWrite == length) ? FP_OK : FP_DEVICE_TIMEOUT_ERROR;
}
