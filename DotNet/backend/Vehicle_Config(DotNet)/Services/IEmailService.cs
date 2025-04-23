using Vehicle_Config_DotNet_.Models;

namespace Vehicle_Config_DotNet_.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequest request);
    }
}
