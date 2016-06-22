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
	public class USERPROFILEPHOTOES
	{
		public int total_count			{ get; set; }	//Total number of profile pictures the target user has
		public PHOTOSIZE[][] photoes	{ get; set; }	//Requested profile pictures (in up to 4 sizes each)
	}
}