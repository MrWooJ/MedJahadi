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
	public class STICKER
	{
		public string file_id		{ get; set; }	//Unique identifier for this file
		public int width			{ get; set; }	//Sticker width
		public int height			{ get; set; }	//Sticker height
		public PHOTOSIZE thumb		{ get; set; }	//Optional. Sticker thumbnail in .webp or .jpg format
		public int file_size		{ get; set; }	//Optional. File size
	}
}