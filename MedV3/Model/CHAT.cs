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
	public class CHAT 
	{
		public int id 				{ get; set; }	//Unique identifier for this chat, not exceeding 1e13 by absolute value
		public string type			{ get; set; }	//Type of chat, can be either “private”, or “group”, or “channel”
		public string title 		{ get; set; }	//Optional. Title, for channels and group chats
		public string username 		{ get; set; }	//Optional. Username, for private chats and channels if available
		public string first_name	{ get; set; }	//Optional. First name of the other party in a private chat
		public string last_name		{ get; set; }	//Optional. Last name of the other party in a private chat
	}
}