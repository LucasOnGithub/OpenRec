﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using api;

namespace server
{
	// Token: 0x02000050 RID: 80
	internal class ImageServer
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00006D1C File Offset: 0x00004F1C
		public ImageServer()
		{
			try
			{
				Console.WriteLine("ImageServer.cs has started.");
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
			this.listener.Prefixes.Add("http://localhost:20182/");
			for (; ; )
			{
				this.listener.Start();
				Console.WriteLine("ImageServer.cs is listening.");
				HttpListenerContext context = this.listener.GetContext();
				HttpListenerRequest request = context.Request;
				HttpListenerResponse response = context.Response;
				string rawUrl = request.RawUrl;
				string text;
				byte[] i = File.ReadAllBytes("SaveData\\profileimage.png");
				using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
				{
					text = streamReader.ReadToEnd();
				}
				Console.WriteLine("Image Requested: " + rawUrl);
				Console.WriteLine("Image Data: " + text);
				Console.WriteLine("Image Response: ");
				byte[] bytes = i;
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
