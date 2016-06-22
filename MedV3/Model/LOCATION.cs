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
	public class LOCATION 
	{
		public float longitude		{ get; set; }	//Longitude as defined by sender
		public float latitude		{ get; set; }	//latitude as defined by sender
	}
}