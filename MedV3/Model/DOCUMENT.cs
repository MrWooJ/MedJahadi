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
	public class DOCUMENT
	{
		public string file_id		{ get; set; }	//Unique identifier for this file
		public PHOTOSIZE thumb		{ get; set; }	//Optional. Document thumbnail as defined by sender
		public string file_name		{ get; set; }	//Optional. Original filename as defined by sender
		public string mime_type		{ get; set; }	//Optional. MIME type of the file as defined by sender
		public int file_size		{ get; set; }	//Optional. File size
	}
}