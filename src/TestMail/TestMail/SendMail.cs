using System;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace TestMail
{
	public class SendMail
	{
	public string Server {private set;get;}
	public string UserName {private set;get;}
	string Password {set;get;}
	public int Port {private set;get;}
	public bool EnableSSL {private set;get;}
	bool Credentials;
	SmtpClient SmtpClient = null;
	public SendMail(string server,int port,bool enableSSL){
	Server = server;	
	Port = port;
	EnableSSL = enableSSL;
	}
	public SendMail(string server,int port,bool enableSSL,string username,string password): 
		this(server,port,enableSSL){
	UserName = username;
	Password = password;
	Credentials = true;
	}
	
	public void Send(MailMessage msg){
	SmtpClient = new SmtpClient(Server,Port);
		SmtpClient.EnableSsl = EnableSSL;
		//DO NOT use: this property throws a Exception
		//SmtpClient.UseDefaultCredentials = true;
		if(Credentials){
		SmtpClient.UseDefaultCredentials = false;
		SmtpClient.Credentials = new NetworkCredential(UserName,Password);
		}
	ServicePointManager.ServerCertificateValidationCallback = 
     delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
     { return true; };
	SmtpClient.Send(msg);
	}
	}
}