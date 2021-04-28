using Email.Abstract;
using System;
using System.Net;
using System.Net.Mail;

namespace Email.Concrete
{
    public class GmailEmailSender : IEmailSender
    {
        private SmtpClient smtpClient = new SmtpClient();
        public GmailEmailSender()
        {
            SetConfigurations();
        }

        /// <summary>
        /// Smtp ayarlarını barındırır.
        /// </summary>
        private void SetConfigurations()
        {
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            // Mail Gönderecek Olan Adres ve Şifresi
            smtpClient.Credentials = new NetworkCredential("mail@gmail.com", "Şifre");
        }

        public void Send(string message)
        {
            Console.WriteLine("Email gönderme işlemi başladı.");
            try
            {
                MailMessage mail = new MailMessage();

                // Mail Gönderecek Olan Adres ve Başlık
                mail.From = new MailAddress("mail@gmail.com", "ComputerWorkingStatusMonitor-Notificator");

                // Konu
                mail.Subject = "Haber Alınamıyor!";

                mail.IsBodyHtml = true;

                // Mesaj içeriği
                mail.Body = message;

                // Mail Gönderilecek Adresler
               mail.To.Add("hedefmail@hotmail.com");

                smtpClient.Send(mail);

                Console.WriteLine(DateTime.Now + " mail gönderildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
