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
using MySql.Data.MySqlClient; 
using TGModels;

namespace TGDataBaseManager
{
	public class DBManager
	{
		public static string connectionString = @"server=localhost;userid=root;password=root;database=MedJahadiDB;Charset=utf8";

		public static MySqlConnection connection = null;
		public static MySqlDataReader dataReader = null;
		public static MySqlTransaction Transaction = null; 

		public static int getUserLatestLevel(string userId)
		{
			int val = 0;
			try
			{
				connection = new MySqlConnection(connectionString);
				connection.Open();
				string stm = "SELECT Level AS LEVEL FROM VolunteerTableV3 WHERE UserIdentifier=" + userId;
				MySqlCommand cmd = new MySqlCommand(stm, connection);
				dataReader = cmd.ExecuteReader();
				Console.WriteLine("Query: "+stm);                

				if (!dataReader.HasRows)
				{
					val = 0;
				}
				else
				{
					dataReader.Read();
					string lev = dataReader["LEVEL"].ToString();
					if (lev.Length != 0)
					{
						Console.WriteLine("Lev: " + lev);
						decimal level = System.Convert.ToDecimal(lev);
						val = (int) level;
					}
					else
					{
						val = 0;
					}
				}

			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error: {0}",  ex.ToString());
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return val;
		}        


		public static bool CheckUserExists(string userId)
		{
			bool val = false;
			try
			{
				connection = new MySqlConnection(connectionString);
				connection.Open();
				string stm = "SELECT UserIdentifier AS UID FROM VolunteerTableV3 WHERE UserIdentifier=" + userId;
				MySqlCommand cmd = new MySqlCommand(stm, connection);
				dataReader = cmd.ExecuteReader();

				if (dataReader.HasRows)
				{
					val = true;
				}
				else
				{
					val = false;
				}
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error Check UserExists: {0}",  ex.ToString());
				val = false;
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return val;
		}  
		public static bool increeseUserLevel(string userId)
		{
			bool val = false;
			try
			{
				connection = new MySqlConnection(connectionString);
				connection.Open();
				
				string stm = "SELECT Level AS LEVEL FROM VolunteerTableV3 WHERE UserIdentifier=" + userId;
				MySqlCommand cmd = new MySqlCommand(stm, connection);
				dataReader = cmd.ExecuteReader();

				int levelNum = 0;
				if (dataReader.HasRows)
				{
					dataReader.Read();

					string lev = dataReader["LEVEL"].ToString();
					levelNum = (int) System.Convert.ToDecimal(lev) + 1;
				}
				dataReader.Close();
				
				MySqlCommand cmd1 = new MySqlCommand();
				cmd1.Connection = connection;

				cmd1.CommandText = "UPDATE VolunteerTableV3 SET Level="+levelNum.ToString()+" WHERE UserIdentifier=" + userId;
				cmd1.ExecuteNonQuery();
				val = true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error Increesing Level: {0}",  ex.ToString());
				val = false;
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return val;
		}

		public static bool CreateUser(string userId)
		{
			bool val = false;
			try
			{
				bool exists = CheckUserExists(userId);
				if (exists)
				{
					val = false;
					return val;
				}
				connection = new MySqlConnection(connectionString);
				connection.Open();
				
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = connection;

				cmd.CommandText = "INSERT INTO VolunteerTableV3(UserIdentifier , Level) VALUES(@UserIdentifier , @Level)";
				cmd.Prepare();

				cmd.Parameters.AddWithValue("@UserIdentifier", userId);
				cmd.Parameters.AddWithValue("@Level", "0");
				cmd.ExecuteNonQuery();
				val = true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error Creating User: {0}",  ex.ToString());
				val = false;
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return val;
		}

		public static bool SetupFullRegister(string userId, string firstname, string lastname, string fathersname,
			string gender, string marrige, string cellPhone, string nationalCode, string birthPlace, string birthDate, 
			string universityCourse, string universityPlace, string courseDegree)
		{
			bool val = false;
			try
			{
				connection = new MySqlConnection(connectionString);
				connection.Open();
				
				MySqlCommand cmd1 = new MySqlCommand();
				cmd1.Connection = connection;

				string minQuery = "Firstname=\""+firstname+"\""+", Lastname=\""+lastname+"\""+", Fathersname=\""+fathersname+"\""+", Gender=\""+gender+"\""+", Marriage=\""+marrige+"\""+", CellPhone=\""+cellPhone+"\""+", NationalCode=\""+nationalCode+"\""+", BirthPlace=\""+birthPlace+"\""+", BirthDate=\""+birthDate+"\""+", UniversityCourse=\""+universityCourse+"\""+", UniversityPlace=\""+universityPlace+"\""+", CourseDegree=\""+courseDegree+"\"";
				string query = "UPDATE VolunteerTableV3 SET "+ minQuery + " WHERE UserIdentifier=" + userId;
				Console.WriteLine("Query: " +query);
				byte[] bytes = Encoding.Default.GetBytes(query);
				query = Encoding.UTF8.GetString(bytes);
				cmd1.CommandText = query;
				cmd1.ExecuteNonQuery();
				val = true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error Set Personality: {0}",  ex.ToString());
				val = false;
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return val;
		}

		public static bool Update(string userId, int field, string value)
		{
			bool val = false;
			try
			{
				connection = new MySqlConnection(connectionString);
				connection.Open();

				MySqlCommand cmd1 = new MySqlCommand();
				cmd1.Connection = connection;

				string stringField = FindFieldString(field);

				string minQuery = stringField +"=\""+value+"\"";
				string query = "UPDATE VolunteerTableV3 SET "+ minQuery +" WHERE UserIdentifier=" + userId;
				cmd1.CommandText = query;
				cmd1.ExecuteNonQuery();
				val = true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error While Updating: {0}",  ex.ToString());
				val = false;
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return val;
		}

		public static string FindFieldString(int field)
		{
			switch (field)
			{
				case 1:
				return "Firstname";
				break;
				case 2:
				return "Lastname";
				break;
				case 3:
				return "Fathersname";
				break;
				case 4:
				return "Gender";
				break;
				case 5:
				return "Marriage";
				break;
				case 6:
				return "CellPhone";
				break;
				case 7:
				return "NationalCode";
				break;
				case 8:
				return "BirthPlace";
				break;
				case 9:
				return "BirthDate";
				break;
				case 10:
				return "UniversityCourse";
				break;
				case 11:
				return "UniversityPlace";
				break;
				case 12:
				return "CourseDegree";
				break;

				default:
				break;
			}
			return null;
		}

		public static bool ResetOrDispose(string userId)
		{
			bool val = false;
			try
			{
				connection = new MySqlConnection(connectionString);
				connection.Open();

				MySqlCommand cmd1 = new MySqlCommand();
				cmd1.Connection = connection;

				cmd1.CommandText = "DELETE FROM VolunteerTableV3 WHERE UserIdentifier="+userId;
				cmd1.ExecuteNonQuery();
				val = true;
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error: {0}",  ex.ToString());
				val = false;
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return val;
		}

		public static NameValueCollection getFullUsersData(string userId)
		{
			NameValueCollection nvc = new NameValueCollection();
			try
			{
				connection = new MySqlConnection(connectionString);
				connection.Open();
				string stm = "SELECT * FROM VolunteerTableV3 WHERE UserIdentifier=" + userId;
				MySqlCommand cmd = new MySqlCommand(stm, connection);
				dataReader = cmd.ExecuteReader();
				if (dataReader.HasRows)
				{ 
					dataReader.Read();               
					nvc.Add("Firstname", dataReader.GetString(2));
					nvc.Add("Lastname", dataReader.GetString(3));
					nvc.Add("Fathersname", dataReader.GetString(4));
					nvc.Add("Gender", dataReader.GetString(5));
					nvc.Add("Marriage", dataReader.GetString(6));
					nvc.Add("CellPhone", dataReader.GetString(7));
					nvc.Add("NationalCode", dataReader.GetString(8));
					nvc.Add("BirthPlace", dataReader.GetString(9));
					nvc.Add("BirthDate", dataReader.GetString(10));
					nvc.Add("UniversityCourse", dataReader.GetString(11));
					nvc.Add("UniversityPlace", dataReader.GetString(12));
					nvc.Add("CourseDegree", dataReader.GetString(13));
				}
			}
			catch (MySqlException ex)
			{
				Console.WriteLine("Error Fetching UserData: {0}",  ex.ToString());
			}
			finally 
			{
				if (dataReader != null) 
				dataReader.Close();

				if (connection != null)
				connection.Close();
			}
			return nvc;
		}        

	}
}
