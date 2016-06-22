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
	public class Person 
	{
        public string firstname                     { get; set; }
        public string lastname                      { get; set; }
        public string fathersname                   { get; set; }
        
        public bool isMale                          { get; set; }
        
        public bool isMarriged                      { get; set; }
        
        public string birthCertificateNumber        { get; set; }
        public string nationalCodeNumber            { get; set; }
        public string birthdayPlace                 { get; set; }
        public string birthdayDate                  { get; set; }
        
        public string UniversityCourse              { get; set; }
        public string UniversityTitle               { get; set; }
        public string UniversityDegree              { get; set; }
        
        public string HomeAddress                   { get; set; }
        public string HomePhoneNumber               { get; set; }
        public string cellPhoneNumber               { get; set; }
        
        public string WorkAddress                   { get; set; }
        public string WorkPhoneNumber               { get; set; }

        public int userLevel                        { get; set; }        
	}
}