
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace  WebApplication2.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task<string> GetSendEmailAsync(MailRequest mailRequest);
        Task<string> SendWelcomeEmailAsync(WelcomeRequest mailRequest); 
    }

    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<string> GetSendEmailAsync(MailRequest mailRequest)
        {
            var em = "";
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                em = email.To.ToString();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }

                Console.WriteLine(mailRequest.Body);

                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "Mail sent to " + email.To.ToString();

            }
            catch (Exception ex)
            { 
                return $"Could not sent email to " + em.ToString();
            }

        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }

                Console.WriteLine(mailRequest.Body);

                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true); 

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        }
        public async Task<string> SendWelcomeEmailAsync3(WelcomeRequest request) {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                //if (request.Attachments != null)
                //{
                //    byte[] fileBytes;
                //    foreach (var file in request.Attachments)
                //    {
                //        if (file.Length > 0)
                //        {
                //            using (var ms = new MemoryStream())
                //            {
                //                file.CopyTo(ms);
                //                fileBytes = ms.ToArray();
                //            }
                //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                //        }
                //    }
                //}

                Console.WriteLine(request.Subject);

                builder.HtmlBody = request.Subject;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "Ok";

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return "Err" + ex.Message;
            }
        }
        public async Task<string> SendWelcomeEmailAsync(WelcomeRequest request)
        {
            try
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory()  , "..\\WebApplication2.Library\\Models\\Templates\\WelcomeTemplate.html");
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                //request.Link = request.Link.Replace("localhost:7209", "tconx.online/tt");
                MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail).Replace("[link]", request.Link)
                    .Replace("[date]", DateTime.Now.ToShortDateString()).Replace("[time]", DateTime.Now.ToShortTimeString())
                    .Replace("[docno]", DateTime.Now.Ticks.ToString()).Replace("[linktext]", request.LinkText)
                    .Replace("[linkcommand]", request.LinkCommand).Replace("[subject]", request.Subject);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(request.ToEmail));
                email.Subject = $"Welcome {request.UserName}";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();

                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return "sent";
            }
            catch (Exception ex)
            {
                return $"notsent {ex.Message + "," + ex.InnerException}";
            }

        }
    
    }

}
