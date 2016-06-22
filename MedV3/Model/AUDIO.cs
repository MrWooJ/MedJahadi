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
	public class AUDIO
	{
		public string file_id		{ get; set; }	//Unique identifier for this file
		public int duration			{ get; set; }	//Duration of the audio in seconds as defined by sender
		public string performer		{ get; set; }	//Optional. Performer of the audio as defined by sender or by audio tags
		public string title 		{ get; set; }	//Optional. Title of the audio as defined by sender or by audio tags
		public string mime_type		{ get; set; }	//Optional. MIME type of the file as defined by sender
		public int file_size		{ get; set; }	//Optional. File size
	}
}