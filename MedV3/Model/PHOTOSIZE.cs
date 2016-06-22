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
	public class PHOTOSIZE
	{
		public string file_id		{ get; set; }	//Unique identifier for this file
		public int width			{ get; set; }	//Photo width
		public int height			{ get; set; }	//Photo height
		public int file_size		{ get; set; }	//Optional. File size
	}
}