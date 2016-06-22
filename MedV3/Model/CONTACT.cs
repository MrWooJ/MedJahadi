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
	public class CONTACT 
	{
		public string phone_number	{ get; set; }	//Contact's phone number
		public string first_name	{ get; set; }	//Contact's first name
		public string last_name		{ get; set; }	//Optional. Contact's last name
		public int user_id			{ get; set; }	//Optional. Contact's user identifier in Telegram
	}
}