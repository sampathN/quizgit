using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;

public class emailer
{
    string mailerresult = "";

    //constructor    
	public emailer()
	{
        
	}

    public string contacthelp(string name, string mailsender, string mailreceiver, string mailmessage)
    {
        MailMessage message = new MailMessage();

        string emailStyles = "<style type='text/css'>" +
                              "body {width:95%; float:left;font-family:arial; font-size:12px; text-align:center; color:#6a737b;}" +
                              "a{text-decoration:none;}" +
                              "a:hover{color:red;}" +
                              "#wrapper{color:#455560; margin:auto; width:750px; text-align:left;}" +
                              "#header{float:left; margin:0; padding:0;}" +
                              "#content{width:100%; float:left; clear:both; padding:30px 0; margin:0;}" +
                              "#content p{width:650px; text-align:left; margin:0}" +
                              "#footer{float:left; margin:0; padding:0;}" +                              
                              ".bold{font-weight:bold;}" +
                              "</style>";

        string htmlBody = emailStyles +
                            "<body>" +
                                "<div id='wrapper'>" +
                                    "<div id='content'>" +
                                        "<p>" +
                                           "Hello " + name + ",<br /><br />" +
                                           "Thanks for completing the quiz.<br /><br />" +                                           
                                        "</p>" +
                                    "</div>" +
                                "</div>" +
                            "</body>";


        AlternateView Html = AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html");
        message.From = (new MailAddress(mailsender));
        message.To.Add(new MailAddress(mailreceiver));
        message.Sender = (new MailAddress(mailsender));
        message.Subject = ("Thanks for taking the Quiz");
        message.AlternateViews.Add(Html);
        SmtpClient client = new SmtpClient();
        client.Host = "127.0.0.1"; //IP address of server
        client.Port = 25;

        try
        {
            client.Send(message); //Send email
        }
        catch(Exception ex)
        {
            //error
            mailerresult = ex.Message.ToString();
        }
        return mailerresult;
    }
}