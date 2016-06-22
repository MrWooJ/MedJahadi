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
	public class USER 
	{
		public int id 				{ get; set; }	//Unique identifier for this user or bot
		public string first_name	{ get; set; }	//User‘s or bot’s first name
		public string last_name		{ get; set; }	//Optional. User‘s or bot’s last name
		public string username		{ get; set; }	//Optional. User‘s or bot’s username
	}
}

