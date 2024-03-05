using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Generic;
using System.Text;
using System.IO; 

namespace TestMail
{
	public class EMailMessage : MailMessage
	{		
		public EMailMessage (string subject,string fromField,List<string> to,string body,
		                     bool isBodyHtml)
		{
			Subject = subject;
			From = new MailAddress(fromField);
			foreach(string item in to) To.Add(new MailAddress(item));
			IsBodyHtml = isBodyHtml;
			if(IsBodyHtml)
				Body = GetHTMLContent(body);
			else
				Body = body;
		}
		
		string GetHTMLContent(string body){
		return new StringBuilder().Append("<html><head><title>")
		.AppendFormat("{0}</title></head>",Subject)
		.AppendFormat("<body><p><h2><font face=\"arial\" color=\"green\">{0}</font></h2></p><body></html>",body).ToString();
		}
	}
}
