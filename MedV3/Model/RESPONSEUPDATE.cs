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
	public class RESPONSEUPDATE
	{
		public string ok 			{ get; set; }	//If ‘ok’ equals true, the request was successful and the result of the query can be found in the ‘result’ field. In case of an unsuccessful request, ‘ok’ equals false and the error is explained in the ‘description’
		public string description	{ get; set; }	//human-readable description of the result
		public int error_code 		{ get; set; }	//
		public UPDATE[] result 		{ get; set; }	//Optional. New incoming message of any kind — text, photo, sticker, etc.
	}
}