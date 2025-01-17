﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace server
{
	// Token: 0x02000050 RID: 80
	internal class NameServer
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00006D1C File Offset: 0x00004F1C
		public NameServer()
		{
			try
			{
				Console.WriteLine("NameServer.cs has started.");
				new Thread(new ThreadStart(this.StartListen)).Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00006D84 File Offset: 0x00004F84
		private void StartListen()
		{
			this.listener.Prefixes.Add("http://localhost:20181/");
			for (; ; )
			{
				this.listener.Start();
				Console.WriteLine("NameServer.cs is listening.");
				HttpListenerContext context = this.listener.GetContext();
				HttpListenerRequest request = context.Request;
				HttpListenerResponse response = context.Response;
				string rawUrl = request.RawUrl;
				string s = "";
				s = "{\"API\":\"http://localhost:2018\",\"Notifications\":\"http://localhost:20161\",\"Images\":\"http://localhost:20182\"}";
				Console.WriteLine("API Response: " + s);
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				response.ContentLength64 = (long)bytes.Length;
				Stream outputStream = response.OutputStream;
				outputStream.Write(bytes, 0, bytes.Length);
				Thread.Sleep(400);
				outputStream.Close();
				this.listener.Stop();
			}
		}

		public static string VersionCheckResponse = "{\"ValidVersion\":true}";

		public static string BlankResponse = "";


		// Token: 0x04000192 RID: 402
		private HttpListener listener = new HttpListener();
	}
}
