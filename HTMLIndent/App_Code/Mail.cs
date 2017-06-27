using System;
using System.Web.Mail;
using System.Configuration;

/// <summary>
/// Summary description for Mail
/// </summary>
public class Mail
{
    public Mail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void SendMe(string to, string cc, string bcc, string subject, string body)
    {
        
            MailMessage Message = new MailMessage();
            Message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            Message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", ConfigurationManager.AppSettings["mailSender"].ToString());
            Message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ConfigurationManager.AppSettings["mailPass"].ToString());

            Message.To = to;
            Message.From = ConfigurationManager.AppSettings["mailSender"].ToString();
            Message.Cc = cc;
            Message.Bcc = bcc;
            Message.Subject = subject;
            Message.Body = body;//.Replace("\n", "<br />");
            Message.BodyFormat = MailFormat.Html;

            //SmtpMail.SmtpServer = ConfigurationManager.AppSettings["mailServer"].ToString();
            //SmtpMail.Send(Message);
         
    }
}