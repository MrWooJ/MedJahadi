using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TGFileManager
{
	public class FileManager
	{
		public static void WriteInTempFile(string path, string content)
		{
			try
	    	{
				using (FileStream fs = File.OpenWrite(path)) 
				{
					Byte[] info = new UTF8Encoding(true).GetBytes(content);
					fs.Write(info, 0, info.Length);
					fs.Close();
				}			
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
		
		public static string ReadFromTempFile(string path)
		{
			try
	    	{
				using (FileStream fs = File.OpenRead(path)) 
				{
					byte[] b = new byte[1024];
					UTF8Encoding temp = new UTF8Encoding(true);
					fs.Read(b,0,b.Length);
					fs.Close();
					
					decimal n;
					bool isNumeric = decimal.TryParse(temp.GetString(b), out n);
					if (isNumeric)
						return temp.GetString(b);
					else
						return "";
				}			
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return "";
		}
	}
}