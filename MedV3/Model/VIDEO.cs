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
using TGModels;

namespace TGModels
{
	public class VIDEO
	{
		public string file_id		{ get; set; }	//Unique identifier for this file
		public int width			{ get; set; }	//Video width as defined by sender
		public int height			{ get; set; }	//Video height as defined by sender
		public int duration			{ get; set; }	//Duration of the video in seconds as defined by sender
		public PHOTOSIZE thumb		{ get; set; }	//Optional. Video thumbnail
		public string mime_type		{ get; set; }	//Optional. Mime type of a file as defined by sender
		public int file_size		{ get; set; }	//Optional. File size
	}
}