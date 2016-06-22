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
	public class FILE
	{
		public string file_id		{ get; set; }	//nique identifier for this file
		public int file_size		{ get; set; }	//Optional. File size, if known
		public string  file_path	{ get; set; }	//Optional. File path. Use https://api.telegram.org/file/bot<token>/<file_path> to get the file.
	}
}