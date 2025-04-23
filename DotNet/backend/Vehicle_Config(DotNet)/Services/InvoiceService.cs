using Vehicle_Config_DotNet_.Models;
using Vehicle_Config_DotNet_.Repositories;

    namespace Vehicle_Config_DotNet_.Services
{
    public class InvoiceService : IInvoiceService
        {
            private readonly ProjectContext _context;

            public InvoiceService(ProjectContext context)
            {
                _context = context;
            }

            public Invoice CreateInvoice(Invoice invoice)
            {
                _context.Invoices.Add(invoice);
                _context.SaveChanges();
                return invoice;
            }
        }
    }

