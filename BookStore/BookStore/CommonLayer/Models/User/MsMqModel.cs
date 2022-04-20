using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models.User
{
    public class MsMqModel
    {
        MessageQueue messageQueue = new MessageQueue(); //install Experimental.System.Messaging package

        public void Sender(string token)
        {
            messageQueue.Path = @".\private$\Tokens"; //widows path 
            try
            {
                if (!MessageQueue.Exists(messageQueue.Path))
                {
                    MessageQueue.Create(messageQueue.Path);
                }
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted; //way to implement delegate in program
                messageQueue.Send(token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("swarajoshi2022@gmail.com", "Swara@2022")
                };
                mailMessage.From = new MailAddress("swarajoshi2022@gmail.com");
                mailMessage.To.Add(new MailAddress("swarajoshi2022@gmail.com"));
                mailMessage.Body = token;
                mailMessage.Subject = "This is Forgot Password Link";
                smtpClient.Send(mailMessage);
            }
            catch (Exception)
            {
                messageQueue.BeginReceive(); //if email not sent then in catch the event get triggerrd again
            }
        }
    }
}

       

