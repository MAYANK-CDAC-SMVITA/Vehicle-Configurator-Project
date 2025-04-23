using Microsoft.AspNetCore.Mvc;
using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Services;
namespace Vehicle_Config_DotNet_.Controllers
    {

        [ApiController]
        [Route("api/[controller]")]
        public class InvoiceController : ControllerBase
        {
            private readonly IInvoiceService _invoiceService;

            public InvoiceController(IInvoiceService invoiceService)
            {
                _invoiceService = invoiceService;
            }

            [HttpGet("home")]
            public IActionResult Greet()
            {
                return Ok("Welcome");
            }

            [HttpPost("invoices")]
            public IActionResult CreateInvoice([FromBody] Invoice invoice)
            {
                var savedInvoice = _invoiceService.CreateInvoice(invoice);
                return CreatedAtAction(nameof(CreateInvoice), new { id = savedInvoice.InvId }, savedInvoice);
            }
        }
    }


