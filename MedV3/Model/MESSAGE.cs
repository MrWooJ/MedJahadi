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
	public class MESSAGE
	{
		public int message_id 				{ get; set; }	//Unique message identifier
		public USER from 					{ get; set; }	//Optional. Sender, can be empty for messages sent to channels
		public int date 					{ get; set; }	//Date the message was sent in Unix time
		public CHAT chat 					{ get; set; }	//Conversation the message belongs to
		public USER forward_from 			{ get; set; }	//Optional. For forwarded messages, sender of the original message
		public int forward_date 			{ get; set; }	//Optional. For forwarded messages, date the original message was sent in Unix time
		public MESSAGE reply_to_message 	{ get; set; }	//Optional. For replies, the original message. Note that the Message object in this field will not contain further reply_to_message fields even if it itself is a reply.
		public string text 					{ get; set; }	//Optional. For text messages, the actual UTF-8 text of the message
		public AUDIO audio 					{ get; set; }	//Optional. Message is an audio file, information about the file
		public DOCUMENT document 			{ get; set; }	//Optional. Message is a general file, information about the file
		public PHOTOSIZE[] photo 			{ get; set; }	//Optional. Message is a photo, available sizes of the photo
		public STICKER sticker 				{ get; set; }	//Optional. Message is a sticker, information about the sticker
		public VIDEO video 					{ get; set; }	//Optional. Message is a video, information about the video
		public VOICE voice 					{ get; set; }	//Optional. Message is a voice message, information about the file
		public string caption 				{ get; set; }	//Optional. Caption for the photo or video
		public CONTACT contact 				{ get; set; }	//Optional. Message is a shared contact, information about the contact
		public LOCATION location 			{ get; set; }	//Optional. Message is a shared location, information about the location
		public USER new_chat_participant 	{ get; set; }	//Optional. A new member was added to the group, information about them (this member may be bot itself)
		public USER left_chat_participant 	{ get; set; }	//Optional. A member was removed from the group, information about them (this member may be bot itself)
		public string new_chat_title 		{ get; set; }	//Optional. A chat title was changed to this value
		public PHOTOSIZE[] new_chat_photo 	{ get; set; }	//Optional. A chat photo was change to this value
		public bool delete_chat_photo 		{ get; set; }	//Optional. Informs that the chat photo was deleted
		public bool group_chat_created 		{ get; set; }	//Optional. Informs that the group has been created
	}
}