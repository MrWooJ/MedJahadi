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
	public class GETUPDATES
	{
		public UPDATE[] updates 	{ get; set; }	//Optional. New incoming message of any kind — text, photo, sticker, etc.
	}
}