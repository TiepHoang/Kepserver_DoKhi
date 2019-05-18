using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace LibraryExtentions
{
    public class EmailSender
    {
        public static KeyValuePair<bool, string> Send(MailAddress to, string Subject, string Body, params Attachment[] attachments)
        {
            return SendCC(to, Subject, Body, null, null, attachments).GetAwaiter().GetResult();
        }

        public static async Task<KeyValuePair<bool, string>> SendCC(MailAddress to, string Subject, string Body, List<string> CC, List<string> Bcc, params Attachment[] attachments)
        {
            try
            {
                //string username = ConfigurationManager.AppSettings["myhotel.demo@gmail.com"];
                //string password = ConfigurationManager.AppSettings["zxc000zxc"];
                string username = "myhotel.demo@gmail.com";
                string password = "zxc000zxc";
                using (SmtpClient mySmtpClient = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(username, password),
                    EnableSsl = true
                })
                {
                    MailAddress from = new MailAddress("myhotel.demo@gmail.com", "PHẦN MỀM QUẢN LÝ");
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to)
                    {
                        Subject = Subject,
                        SubjectEncoding = System.Text.Encoding.UTF8,
                        Body = Body,
                        BodyEncoding = System.Text.Encoding.UTF8,
                        IsBodyHtml = true,
                    };

                    foreach (var item in attachments)
                    {
                        myMail.Attachments.Add(item);
                    }

                    if (CC != null && CC.Count > 0) CC.ForEach(item => myMail.CC.Add(item));
                    if (Bcc != null && Bcc.Count > 0) Bcc.ForEach(item => myMail.Bcc.Add(item));

                    await mySmtpClient.SendMailAsync(myMail);
                    return new KeyValuePair<bool, string>(true, $"send mail to {myMail.To} SUCCESS ({myMail.CC.Count}CC | {myMail.Bcc.Count}Bcc)");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, $"send mail to {to} Error ({CC.Count}CC | {Bcc.Count}Bcc). {ex.TryGetMessage()}");
            }
        }

        public static KeyValuePair<bool, string> SendUseSetting(MailAddress to, string Subject, string BodyHtml, params Attachment[] attachments)
        {
            try
            {
                string username = ConfigurationManager.AppSettings["mail_username"];
                string password = ConfigurationManager.AppSettings["mail_password"];
                string host = ConfigurationManager.AppSettings["mail_host"];
                using (SmtpClient mySmtpClient = new SmtpClient(host)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(username, password),
                    EnableSsl = false
                })
                {
                    MailAddress from = new MailAddress(username, "PHẦN MỀM QUẢN LÝ");
                    MailMessage myMail = new System.Net.Mail.MailMessage(from, to)
                    {
                        Subject = Subject,
                        SubjectEncoding = System.Text.Encoding.UTF8,
                        Body = BodyHtml,
                        BodyEncoding = System.Text.Encoding.UTF8,
                        IsBodyHtml = true
                    };

                    foreach (var item in attachments)
                    {
                        myMail.Attachments.Add(item);
                    }
                    mySmtpClient.Send(myMail);
                    return new KeyValuePair<bool, string>(true, "SUCCESS");
                }
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.TryGetMessage());
            }
        }
    }
}
