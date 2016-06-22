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
	public class REPLAYKEYBOARDMARKUP
	{
		public string[,] keyboard		{ get; set; }	//Array of button rows, each represented by an Array of Strings
		public bool resize_keyboard		{ get; set; }	//Optional. Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of buttons). Defaults to false, in which case the custom keyboard is always of the same height as the app's standard keyboard.
		public bool one_time_keyboard	{ get; set; }	//Optional. Requests clients to hide the keyboard as soon as it's been used. Defaults to false.
		public bool selective			{ get; set; }	//Optional. Use this parameter if you want to show the keyboard to specific users only. Targets: 1) users that are @mentioned in the text of the Message object; 2) if the bot's message is a reply (has reply_to_message_id), sender of the original message.
	}
}