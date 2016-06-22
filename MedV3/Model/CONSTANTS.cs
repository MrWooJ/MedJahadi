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
	public class CONSTANTS
	{
		public static string TempFilePath = "/Meta-Data/temp.txt" ;
		public static string Markdown = "Markdown" ;

		public static string Start = "برای پیوستن به فعالیت های درمانی و یا آموزشی ما ثبت نام کنید.\nبرنامه های در حال اجرای گروه:\n۱-فعالیت های درمانی و آموزشی در مناطق محروم تهران به صورت هفته ای و هر جمعه\n۲-فعالیت های درمانی و آموزشی در استان خوزستان نوروز ۹۵ به مدت ۹-۱۰ روز\n-----\nبرای کسب اطلاعات بیشتر از گروه و دریافت اطلاع رسانی ها از طریق شبکه اطلاع رسانی درمان و آموزش جهادی @MedHelp و یا شماره تلفن های ۰۹۲۱۱۴۴۳۹۸۰ و ۰۲۱۷۷۶۳۹۷۶۶ اقدام فرمائید.\nاز همکاری شما سپاسگذاریم.";
		public static string SignUpQuote = "لطفا در هنگام ثبت نام به موارد زیر توجه فرمائید.\n\n۱- تمام ورودی ها (چه متن و چه عدد) را حتما با حروف فارسی وارد نمائید.\n\n۲- تمام اطلاعات ورودی را باید با فرمت خواسته شده وارد نمائید.\n\n۳- در صورتی که هر یک از فیلد هارا مجبور شدید خالی بگذارید از خط تیره استفاده نمائید.\n\nبرای ادامه بنویسید: ادامه\nبرای انصراف بنویسید: انصراف" ;
		
		public static string SetSetupQuote = "لطفا فرم زیر را دقیقا با فرمت خواسته شده پر نمائید.\n\nنام:\nنام خانوادگی:\nنام پدر:\nجنسیت:\nتاهل:\nشماره موبایل:\nکد ملی:\nمحل سکونت:\nتاریخ تولد:\nرشته تحصیلی:\nدانشگاه:\nمقطع: در صورت تحصیل ترم چندم";

		public static string StartAlready = "شما قبلا شروع کرده اید!" ;
		public static string Dispose = "آیا قصد انصراف از اردوی جهادی را دارید؟\n\nبرای بله بنویسید: بله\nبرای خیر بنویسید: نه" ;
		public static string ShouldStartSignUp = "شما ابتدا باید فرم ثبت نام را پر کنید." ;
		public static string ShouldEdit = "برای اصلاح اطلاعات لطفا همانند الگوی زیر عمل کنید.\n\nشماره ی مورد اصلاح\nمقدار جدید برای اصلاح کردن\n\nبرای مثال:\n۲\nمحمدرضا" ;
		public static string ShouldCompleteSignUp = "لطفا ثبت نام خود را کامل کنید." ;

		public static string SuccessfullEdit = "فیلد مورد نظر با موفقیت اصلاح شد." ;
		public static string UnSuccessfullEdit = "اصلاح موفقیت آمیز نبود!\nلطفا مجددا تلاش کنید." ;
		public static string WrongInput = "اطلاعات با فرمت خواسته شده سازگاری ندارد.\nلطفا مجددا تلاش کنید." ;
		public static string Yes = "بله" ;
		public static string No = "نه" ;
		public static string AgreeMessage = "ادامه" ;
		public static string DisagreeMessage = "انصراف" ;
		public static string SetPersonality = "لطفا اطلاعات شخصی خود را با فرمت زیر وارد نمائید.\n\nنام\nنام خانوادگی\nنام پدر\n\nبرای مثال:\nعلیرضا\nبصیری\nمحمدکاظم" ;
		public static string DisagreeQuote = "از اینکه با ما همراه بودید سپاسگذاریم." ;


		public static string SetGender = "لطفا جنسیت خود را تعیین کنید.\n\nبرای مرد بنویسید: مرد\nبرای زن بنویسید: زن" ;
		public static string SetMarrige = "لطفا وضعیت تاهل خود را مشخص کنید.\n\nبرای مجرد بنویسید: مجرد\nبرای متاهل بنویسید: متاهل" ;
		public static string Man = "مرد" ;
		public static string Woman = "زن" ;

		public static string Marriged = "متاهل" ;
		public static string NotMarriged = "مجرد" ;
		public static string SetUserInfo = "لطفا اطلاعات خواسته شده را به دقت با فرمت زیر وارد نمائید.\n\nشماره همراه\nکد ملی\nمحل سکونت\nتاریخ تولد\n\nبرای مثال:\n۰۹۱۲۹۹۹۸۸۷۷\n۳۷۰۹۴۰۵۴۷\nتهران\n۲۲/۳/۶۸" ;
		public static string SetEducationInfo = "لطفا اطلاعات تحصیلی خود را با فرمت خواسته شده وارد نمائید.\n\nرشته تحصیلی\nدانشگاه\nمدرک تحصیلی\n\nبرای مثال\nدندانپزشکی\nدانشگاه علوم پزشکی تهران\nدکتری" ;

		public static string SetHomeInfo = "لطفا اطلاعات محل زندگی خود را به دقت با فرمت زیر وارد نمائید.\n\nادرس منزل\nشماره تلفن ثابت منزل\nشماره تلفن همراه\n\nبرای مثال:\nتهران-بلوار آیت الله کاشانی- خیابان شهید احمدی-ساختما ستاره-پلاک ۵-واحد۳\n۴۴۲۳۸۷۶۱\n۰۹۱۲۹۹۹۸۸۷۷" ;
		public static string SetWorkInfo = "لطفا اطلاعات محل کار خود را به دقت با فرمت زیر وارد نمائید.\n\nآدرس محل کار\nشماره تلفن ثابت محل کار\n\nبرای مثال:\nتهران-خیابان کریم خان زند-خیابان ایرانشهر-ساختمان پزشکان محب-واحد۳۰۳\n۲۳۲۲۴۵۶۵" ;

		public static string Validating = "آیا اطلاعات وارد شده مورد تائید شما میباشد؟\n\nبرای تائید بنویسید: درست\nبرای اصلاح بنویسید: اصلاح" ;

		public static string SuccessfullSignUp = "عملیات ثبت نام با موفقیت انجام شد.\nبا توجه به تقاضای بالای ثبت نام اطلاعات شما در کارگروه درمانی مربوطه بررسی شده و در اسرع وقت با شما تماس گرفته خواهد شد.\nهمچنین برای کسب اطلاعات بیشتر از گروه و دریافت اطلاع رسانی ها از طریق \"شبکه اطلاع رسانی درمان جهادی @MedHelp\" و یا شماره تلفن های ۰۹۲۱۱۴۴۳۹۸۰ و ۰۲۱۷۷۶۳۹۷۶۶ اقدام فرمائید.\nاز همکاری شما سپاسگذاریم.";

		public static string ContentValidated = "درست" ;
		public static string ContentShouldEdit = "اصلاح" ;

		public static string SignUpButton = "/signup - ثبت نام" ;
		public static string GalleryButton = "/gallery - گالری عکس ها" ;
		public static string DisposeButton = "/dispose - ریست و انصراف" ;
		public static string EditButton = "/edit - اصلاح اطلاعات" ;
		public static string TravelGuide = "/travelGuide - راهنما سفر" ;

		public static string TravelGuideQuote = "گروه درمانی امام رضا طبق سنوات گذشته در نوروز ۹۵ اقدام به برگزاری سفر جهادی در حوزه پزشکی مینماید.\nدر این سفر گروه های داوطلب تحت محور های درمان و آموزش خدمات حوزه ی سلامت رابه مردم محروم روستا های استان خوزستان ارائه میدهند.\n\nاز میان خدمات گروه\nمیتوان به خدمات دندانپزشکی\nپزشکی\nمامایی\nو آموزش بهداشت\nاشاره کرد.\nزمان: ۴ الی ۱۲ فروردین ماه\nوسیله نقلیه رفت و برگشت: اتوبوس\nمحل اسکان: آبادان\nجزئیات دقیق تر مطابق اعلام خواهد شد.";
	}
}
