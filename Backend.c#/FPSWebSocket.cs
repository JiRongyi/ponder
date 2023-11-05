/*
 * Created by SharpDevelop.
 * User: Jerry
 * Date: 23-10-30
 * Time: 03:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPServer {
	/// <summary>
	/// Description of FPSWebSocket.
	/// </summary>
	public class FPSWebSocket {
		private HttpListener listener;
		private const string WebSocketPath = "/ws";
		public FPSWebSocket(string httpUrl) {
			listener = new HttpListener();
			listener.Prefixes.Add(httpUrl);
		}
		public async Task Start() {
			listener.Start();
			Console.WriteLine("WebSocket server started at {listener.Prefixes[0]}");

			while (true) {
				var context = await listener.GetContextAsync();
				if (context.Request.IsWebSocketRequest) {
					var webSocketContext = await context.AcceptWebSocketAsync(null);
					HandleWebSocket(webSocketContext.WebSocket);
				} else {
					context.Response.StatusCode = 400;
					context.Response.Close();
				}
			}
		}

		private async Task HandleWebSocket(WebSocket webSocket) {
			try {
				// You can encapsulate your C++ device logic here.
				// Send and receive messages with the WebSocket client.
				var buffer = new byte[1024];

				while (webSocket.State == WebSocketState.Open) {
					var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

					if (result.MessageType == WebSocketMessageType.Text) {
						var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
						Console.WriteLine("Received: {message}");

						// Implement your C++ device logic here and send responses back.
						// For example, you can use interop to call C++ functions.
						var response = "Device response"; // Replace with your C++ logic.
						var responseBuffer = Encoding.UTF8.GetBytes(response);

						await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
					}
				}
			} catch (Exception ex) {
				Console.WriteLine("WebSocket error: {ex.Message}");
			} finally {
				webSocket.Dispose();
			}
		}
	}
}
