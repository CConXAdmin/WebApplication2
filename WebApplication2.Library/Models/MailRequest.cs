using Microsoft.AspNetCore.Http;

namespace WebApplication2.Services
{
    public class MailRequest
    {
        public string ToEmail { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public List<IFormFile> Attachments { get; set; } = new List<IFormFile>();
    }
    public class MailSettings
    {
        public string Mail { get; set; } = "";
        public string DisplayName { get; set; } = "";
        public string Password { get; set; } = "";
        public string Host { get; set; } = "";
        public int Port { get; set; } = 0;
    }
    public class WelcomeRequest
    {
        public string Subject { get; set; } = "";
        public string LinkCommand { get; set; } = "";
        public string LinkText { get; set; } = "";

        public string ToEmail { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Link { get; set; } = "";
    }
}
