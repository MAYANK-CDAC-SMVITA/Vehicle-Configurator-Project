using Microsoft.AspNetCore.Mvc;
using Vehicle_Config_DotNet_.Services;
using Vehicle_Config_DotNet_.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Vehicle_Config_DotNet_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailSenderController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailSenderController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmailWithInvoice([FromForm] IFormFile file, [FromForm] string email, [FromForm] string invoiceId)
        {
            if (file == null || string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid email request. No file or email provided.");
            }

            try
            {
                // Prepare the email request object
                var emailRequest = new EmailRequest
                {
                    To = new List<string> { email },
                    Subject = $"Invoice {invoiceId}",
                    Body = "Please find your invoice attached.",
                    Attachments = new List<IFormFile> { file }
                };

                await _emailService.SendEmailAsync(emailRequest);
                return Ok("Invoice email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
