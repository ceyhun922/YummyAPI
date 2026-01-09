
using MimeKit;

namespace YummyUI.Service
{
    public class EmailService : IEmailService
    {
         private readonly string _fromMail = "jeyhun312@gmail.com";
        private readonly string _appPassword = "ssjc aabw cnsf mtsy";
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Yummy", _fromMail));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync(_fromMail, _appPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}