using System.Net.Mail;
using System.Net;
using Vehicle_Config_DotNet_.Models;
using System.IO;
using Vehicle_Config_DotNet_.Configuration;

namespace Vehicle_Config_DotNet_.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings ?? throw new ArgumentNullException(nameof(emailSettings));
        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            if (emailRequest == null || emailRequest.To == null || !emailRequest.To.Any())
            {
                throw new ArgumentException("Invalid email request. Recipient email is required.");
            }

            try
            {
                using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
                {
                    smtpClient.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    smtpClient.EnableSsl = true;

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_emailSettings.From);
                        mailMessage.Subject = emailRequest.Subject;
                        mailMessage.Body = emailRequest.Body;
                        mailMessage.IsBodyHtml = true;

                        foreach (var recipient in emailRequest.To)
                        {
                            mailMessage.To.Add(recipient);
                        }

                        if (emailRequest.Attachments != null && emailRequest.Attachments.Any())
                        {
                            foreach (var file in emailRequest.Attachments)
                            {
                                if (file.Length > 0)
                                {
                                    var stream = file.OpenReadStream();
                                    var attachment = new Attachment(stream, file.FileName);
                                    mailMessage.Attachments.Add(attachment);
                                }
                            }
                        }

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (SmtpException smtpEx)
            {
                throw new InvalidOperationException($"SMTP error: {smtpEx.InnerException?.Message ?? smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
    }

}
