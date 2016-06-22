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
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;
    using TGModels;
    using TGController;
    using TGFileManager;
    using TGServerFileUploader;
    using TGDataBaseManager;

    public enum KEYBOARDTYPE
    {
    	REPLAYKEYBOARDMARKUP = 1,
    	REPLAYKEYBOARDHIDE = 2,
    	FORCEREPLAY = 3
    }

    namespace TGServer
    {
    	public class TGServer
    	{
    		public static string apiToken 	= "148273594:AAFED6k97cfJdGgsFDAIcn2rOhDUo2ztdzs";
    		public static string baseURL	= "https://api.telegram.org/bot";

    		public static string tempFileFullPath = "null";

    		public static void Main(string[] args)
    		{
    			if (args.Length < 1)
    			{
    				Console.WriteLine("Error in Input Arguments! " + args.Length + " are Entered!");
    				System.Environment.Exit(1);
    			}

    			tempFileFullPath = args[0] + CONSTANTS.TempFilePath;
    			GetUpdatesManually();
    		}

    		public static void GetUpdatesResponseHandler(RESPONSEUPDATE res)
    		{

    			if (!res.ok.ToLower().Equals("true"))
    			{
    				Console.WriteLine("Error in Response Object With ErrorCode");
    			}
    			else
    			{		
    				if (res.result.Length == 0)
    				return;

    				foreach (UPDATE update in res.result)
    				{
    					Console.WriteLine(update.update_id.ToString());
    					FileManager.WriteInTempFile(tempFileFullPath, update.update_id.ToString());	
    					MESSAGE msg = update.message;

    					string userIdentifier = msg.from.id.ToString();

    					if (msg.text.ToLower().StartsWith("/start"))
    					{
    						if (DBManager.getUserLatestLevel(userIdentifier) == 0)
    						{
    							string keyboardType = MainKeyboard(false);
    							Controller.SendMessage(userIdentifier, CONSTANTS.Start, CONSTANTS.Markdown, true, 0, keyboardType);                                                                                            
    						}
    						else
    						{
    							Controller.SendMessage(userIdentifier, CONSTANTS.StartAlready, CONSTANTS.Markdown, true, 0, null);
    							int check = DBManager.getUserLatestLevel(userIdentifier);
    							LevelOperation(check, userIdentifier);
    						}
    					}

    					if (msg.text.ToLower().StartsWith("/travelguide"))
    					{
    						string keyboardType = HideKeyboard();
    						Controller.SendMessage(userIdentifier, CONSTANTS.TravelGuideQuote, CONSTANTS.Markdown, true, 0, keyboardType);                                                                                            
    					}

    					if (msg.text.ToLower().StartsWith("/dispose"))
    					{
    						if (DBManager.getUserLatestLevel(userIdentifier) != 0)
    						{
    							string keyboardType = QAKeyboard();
    							Controller.SendMessage(userIdentifier, CONSTANTS.Dispose, CONSTANTS.Markdown, true, 0, keyboardType);                                                                
    						}
    						else
    						{
    							Controller.SendMessage(userIdentifier, CONSTANTS.ShouldStartSignUp, CONSTANTS.Markdown, true, 0, null);
    							int check = DBManager.getUserLatestLevel(userIdentifier);
    							LevelOperation(check, userIdentifier);
    						}
    					}
    					if (msg.text.ToLower().StartsWith("/editInfo"))
    					{
    						if (DBManager.getUserLatestLevel(userIdentifier) == 2)
    						{
    							NameValueCollection nvc = DBManager.getFullUsersData(userIdentifier);
    							string content = ContentProducer(nvc);
    							Controller.SendMessage(userIdentifier, content, CONSTANTS.Markdown, true, 0, null);
    							string keyboardType = QAKeyboard();
    							Controller.SendMessage(userIdentifier, CONSTANTS.ShouldEdit, CONSTANTS.Markdown, true, 0, keyboardType);                                    
    						}
    						else
    						{
    							Controller.SendMessage(userIdentifier, CONSTANTS.ShouldCompleteSignUp, CONSTANTS.Markdown, true, 0, null);
    							int check = DBManager.getUserLatestLevel(userIdentifier);
    							LevelOperation(check, userIdentifier);
    						}
    					}

    					if (msg.text.ToLower().StartsWith("/signup"))
    					{
    						if (DBManager.getUserLatestLevel(userIdentifier) == 0)
    						{
    							string keyboardType = QAKeyboard();
    							Controller.SendMessage(userIdentifier, CONSTANTS.SignUpQuote, CONSTANTS.Markdown, true, 0, keyboardType);
    						}
    						else
    						{
    							Controller.SendMessage(userIdentifier, CONSTANTS.ShouldCompleteSignUp, CONSTANTS.Markdown, true, 0, null);
    							int check = DBManager.getUserLatestLevel(userIdentifier);
    							LevelOperation(check, userIdentifier);
    						}
    					}

    					if (msg.reply_to_message.from.username.Equals("MedJahadi_Bot"))
    					{
    						if (msg.reply_to_message.text.Equals(CONSTANTS.ShouldEdit))
    						{
    							string[] separators = {"\n"};
    							string[] words = msg.text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

    							if (words.Length == 2)
    							{

    								Regex regex = new Regex(@"^[0-9]+$");
    								bool che = Regex.IsMatch(words[0],@"^[آ-ی]*$");
    								if (che)
    								{
    									string arabicnumbers=words[0];
    									string EnglishNumbers="";
    									for (int i = 0; i < arabicnumbers.Length; i++)
    									{
    										EnglishNumbers += char.GetNumericValue(arabicnumbers, i);
    									}
    									int convertednumber=Convert.ToInt32(EnglishNumbers);
    									Console.WriteLine("EditLine: " + convertednumber.ToString());
    									bool enter = DBManager.Update(userIdentifier, convertednumber, words[1]);
    									if (enter)
    									Controller.SendMessage(userIdentifier, CONSTANTS.SuccessfullEdit, CONSTANTS.Markdown, true, 0, null);
    									else
    									Controller.SendMessage(userIdentifier, CONSTANTS.UnSuccessfullEdit, CONSTANTS.Markdown, true, 0, null);   
    								}
    								else
    								{
    									Controller.SendMessage(userIdentifier, CONSTANTS.WrongInput, CONSTANTS.Markdown, true, 0, null);
    									string keyboardType = QAKeyboard();
    									Controller.SendMessage(userIdentifier, CONSTANTS.ShouldEdit, CONSTANTS.Markdown, true, 0, keyboardType);                                                            
    								}
    							}
    							else
    							{
    								Controller.SendMessage(userIdentifier, CONSTANTS.WrongInput, CONSTANTS.Markdown, true, 0, null);
    								string keyboardType = QAKeyboard();
    								Controller.SendMessage(userIdentifier, CONSTANTS.ShouldEdit, CONSTANTS.Markdown, true, 0, keyboardType);                                                            
    							}
    						}
    						if (msg.reply_to_message.text.Equals(CONSTANTS.Dispose))
    						{
    							if (msg.text.Contains(CONSTANTS.Yes))
    							{
    								DBManager.ResetOrDispose(userIdentifier);
    							}
    							else if (msg.text.Contains(CONSTANTS.No))
    							{
    							}
    							else
    							{
    								Controller.SendMessage(userIdentifier, CONSTANTS.WrongInput, CONSTANTS.Markdown, true, 0, null);
    								string keyboardType = QAKeyboard();
    								Controller.SendMessage(userIdentifier, CONSTANTS.Dispose, CONSTANTS.Markdown, true, 0, keyboardType);                                                                
    							}
    						}
    						if (msg.reply_to_message.text.Equals(CONSTANTS.SignUpQuote))
    						{
    							int check = DBManager.getUserLatestLevel(userIdentifier);
    							if (check == 0)
    							{
    								if (msg.text.Contains(CONSTANTS.AgreeMessage))
    								{
    									DBManager.CreateUser(userIdentifier);
    									DBManager.increeseUserLevel(userIdentifier);
    									string keyboardType = QAKeyboard();
    									Controller.SendMessage(userIdentifier, CONSTANTS.SetSetupQuote, CONSTANTS.Markdown, true, 0, keyboardType);
    								}
    								else if (msg.text.Contains(CONSTANTS.DisagreeMessage))
    								{
    									Controller.SendMessage(userIdentifier, CONSTANTS.DisagreeQuote, CONSTANTS.Markdown, true, 0, null);
    								}
    								else 
    								{
    									Controller.SendMessage(userIdentifier, CONSTANTS.WrongInput, CONSTANTS.Markdown, true, 0, null);
    									string keyboardType = QAKeyboard();
    									Controller.SendMessage(userIdentifier, CONSTANTS.SignUpQuote, CONSTANTS.Markdown, true, 0, keyboardType);
    								}
    							}
    							else 
    							{
    								LevelOperation(check, userIdentifier);
    							}
    						}
    						if (msg.reply_to_message.text.Equals(CONSTANTS.SetSetupQuote))
    						{
    							string[] separators = {"\n"};
    							string[] words = msg.text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

    							if (words.Length == 12)
    							{
    								int check = DBManager.getUserLatestLevel(userIdentifier);
    								if (check == 1)
    								{
    									DBManager.SetupFullRegister(userIdentifier, words[0], words[1], words[2], words[3], words[4], words[5], words[6], words[7], words[8], words[9], words[10], words[11]);
    									DBManager.increeseUserLevel(userIdentifier);
    									NameValueCollection nvc = DBManager.getFullUsersData(userIdentifier);
    									string content = ContentProducer(nvc);
    									Controller.SendMessage(userIdentifier, content, CONSTANTS.Markdown, true, 0, null);
    									string keyboardType = QAKeyboard();
    									Controller.SendMessage(userIdentifier, CONSTANTS.Validating, CONSTANTS.Markdown, true, 0, keyboardType);
    								}
    								else
    								{
    									LevelOperation(check, userIdentifier);
    								}
    							}
    							else
    							{
    								Controller.SendMessage(userIdentifier, CONSTANTS.WrongInput, CONSTANTS.Markdown, true, 0, null);
    								string keyboardType = QAKeyboard();
    								Controller.SendMessage(userIdentifier, CONSTANTS.SetSetupQuote, CONSTANTS.Markdown, true, 0, keyboardType);
    							}							
    						}
    						if (msg.reply_to_message.text.Equals(CONSTANTS.Validating))
    						{
    							int check = DBManager.getUserLatestLevel(userIdentifier);
    							if (check == 2)
    							{
    								if (msg.text.Contains(CONSTANTS.ContentValidated))
    								{
    									string keyboardType = HideKeyboard();
    									Controller.SendMessage(userIdentifier, CONSTANTS.SuccessfullSignUp, CONSTANTS.Markdown, true, 0, keyboardType);
    								}
    								else if (msg.text.Contains(CONSTANTS.ContentShouldEdit))
    								{
    									NameValueCollection nvc = DBManager.getFullUsersData(userIdentifier);
    									string content = ContentProducer(nvc);
    									Controller.SendMessage(userIdentifier, content, CONSTANTS.Markdown, true, 0, null);
    									string keyboardType = QAKeyboard();
    									Controller.SendMessage(userIdentifier, CONSTANTS.ShouldEdit, CONSTANTS.Markdown, true, 0, keyboardType);                                    
    								}
    								else 
    								{
    									Controller.SendMessage(userIdentifier, CONSTANTS.WrongInput, CONSTANTS.Markdown, true, 0, null);
    									string keyboardType = QAKeyboard();
    									Controller.SendMessage(userIdentifier, CONSTANTS.Validating, CONSTANTS.Markdown, true, 0, keyboardType);                                    
    								}                        
    							}
    							else
    							{
    								LevelOperation(check, userIdentifier);    
    							}
    						}
    					}
    				}
    			}
    		}

    		public static void GetUpdatesManually()
    		{
    			while (true)
    			{
    				WebRequest req = null;
    				try
    				{						
    					string lastOffset = FileManager.ReadFromTempFile(tempFileFullPath);
    					if (lastOffset.Length == 0)
    					{
    						req = WebRequest.Create(baseURL + apiToken + "/getUpdates");
    					}

    					else
    					{
    						decimal decimalVal = 0;
    						decimalVal = System.Convert.ToDecimal(lastOffset) + 1;
    						string offset = decimalVal.ToString();
    						req = WebRequest.Create(baseURL + apiToken + "/getUpdates?offset=" + offset);
    					}

    					req.UseDefaultCredentials = true;

    					var result = req.GetResponse();
    					Stream stream = result.GetResponseStream();
    					StreamReader reader = new StreamReader(stream);

    					RESPONSEUPDATE res = JsonConvert.DeserializeObject<RESPONSEUPDATE>(reader.ReadToEnd());

    					GetUpdatesResponseHandler(res);

    					req.Abort();

    				}
    				catch (Exception ex)
    				{
    					Console.WriteLine("Error Getting Updates");
    					req.Abort();
    				} 
    				finally 
    				{
    					req.Abort();
    				}
    			}
    		}

    		public static void LevelOperation(int level, string userIdentifier)
    		{
    			string keyboardType = "";
    			switch (level)
    			{
    				case 0:
    				keyboardType = QAKeyboard();
    				Controller.SendMessage(userIdentifier, CONSTANTS.SignUpQuote, CONSTANTS.Markdown, true, 0, keyboardType);

    				break;
    				case 1:
    				keyboardType = QAKeyboard();
    				Controller.SendMessage(userIdentifier, CONSTANTS.SetSetupQuote, CONSTANTS.Markdown, true, 0, keyboardType);

    				break;
    				case 2:
    				NameValueCollection nvc = DBManager.getFullUsersData(userIdentifier);
    				string content = ContentProducer(nvc);
    				Controller.SendMessage(userIdentifier, content, CONSTANTS.Markdown, true, 0, null);
    				keyboardType = QAKeyboard();
    				Controller.SendMessage(userIdentifier, CONSTANTS.Validating, CONSTANTS.Markdown, true, 0, keyboardType);

    				break;
    				default:
    				break;
    			}
    		}

    		public static string ContentProducer(NameValueCollection nav)
    		{
    			string str = "";
    			str = str + "۱-" + "نام: " + nav.Get("Firstname") + "\n";
    			str = str + "۲-" + "نام خانوادگی: " + nav.Get("Lastname") + "\n";
    			str = str + "۳-" + "نام پدر: " + nav.Get("Fathersname") + "\n";
    			str = str + "۴-" + "جنسیت: " + nav.Get("Gender") + "\n";
    			str = str + "۵-" + "تاهل: " + nav.Get("Marriage") + "\n";
    			str = str + "۶-" + "شماره موبایل: " + nav.Get("CellPhone") + "\n";
    			str = str + "۷-" + "کد ملی: " + nav.Get("NationalCode") + "\n";
    			str = str + "۸-" + "محل سکونت: " + nav.Get("BirthPlace") + "\n";
    			str = str + "۹-" + "تاریخ تولد: " + nav.Get("BirthDate") + "\n";
    			str = str + "۱۰-" + "رشته تحصیلی: " + nav.Get("UniversityCourse") + "\n";
    			str = str + "۱۱-" + "دانشگاه: " + nav.Get("UniversityPlace") + "\n";
    			str = str + "۱۲-" + "مدرک: " + nav.Get("CourseDegree") + "\n";
    			return str;
    		}

    		public static string SignUpKeyboard()
    		{
    			REPLAYKEYBOARDMARKUP keyInterface = new REPLAYKEYBOARDMARKUP();
    			keyInterface.keyboard = new string[2, 1] {{CONSTANTS.AgreeMessage},{CONSTANTS.DisagreeMessage}};
    			keyInterface.resize_keyboard = false;
    			keyInterface.one_time_keyboard = false;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;				
    		}

    		public static string YNKeyboard()
    		{
    			REPLAYKEYBOARDMARKUP keyInterface = new REPLAYKEYBOARDMARKUP();
    			keyInterface.keyboard = new string[2, 1] {{CONSTANTS.Yes},{CONSTANTS.No}};
    			keyInterface.resize_keyboard = false;
    			keyInterface.one_time_keyboard = false;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;				
    		}

    		public static string GenderKeyboard()
    		{
    			REPLAYKEYBOARDMARKUP keyInterface = new REPLAYKEYBOARDMARKUP();
    			keyInterface.keyboard = new string[2, 1] {{CONSTANTS.Man},{CONSTANTS.Woman}};
    			keyInterface.resize_keyboard = false;
    			keyInterface.one_time_keyboard = false;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;				
    		}

    		public static string ValidationKeyboard()
    		{
    			REPLAYKEYBOARDMARKUP keyInterface = new REPLAYKEYBOARDMARKUP();
    			keyInterface.keyboard = new string[2, 1] {{CONSTANTS.ContentValidated},{CONSTANTS.ContentShouldEdit}};
    			keyInterface.resize_keyboard = false;
    			keyInterface.one_time_keyboard = false;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;				
    		}

    		public static string MarrigeKeyboard()
    		{
    			REPLAYKEYBOARDMARKUP keyInterface = new REPLAYKEYBOARDMARKUP();
    			keyInterface.keyboard = new string[2, 1] {{CONSTANTS.Marriged},{CONSTANTS.NotMarriged}};
    			keyInterface.resize_keyboard = false;
    			keyInterface.one_time_keyboard = false;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;				
    		}

    		public static string QAKeyboard()
    		{
    			FORCEREPLAY keyInterface = new FORCEREPLAY();
    			keyInterface.force_reply = true;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;				
    		}

    		public static string HideKeyboard()
    		{
    			REPLAYKEYBOARDHIDE keyInterface = new REPLAYKEYBOARDHIDE();
    			keyInterface.hide_keyboard = true;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;
    		}

    		public static string MainKeyboard(bool isRegistered)
    		{
    			REPLAYKEYBOARDMARKUP keyInterface = new REPLAYKEYBOARDMARKUP();
    			if (!isRegistered)
    			keyInterface.keyboard = new string[3, 1] {{CONSTANTS.SignUpButton},{CONSTANTS.DisposeButton},{CONSTANTS.TravelGuide}};
    			else
    			keyInterface.keyboard = new string[3, 1] {{CONSTANTS.EditButton},{CONSTANTS.DisposeButton},{CONSTANTS.TravelGuide}};
    			keyInterface.resize_keyboard = false;
    			keyInterface.one_time_keyboard = false;
    			keyInterface.selective = false;
    			string json = JsonConvert.SerializeObject(keyInterface);
    			return json;				
    		}
    	}
    }
