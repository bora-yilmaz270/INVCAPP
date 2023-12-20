using INVCAPP.Core.Models;

namespace INVCAPP.Core.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice> AddInvoiceAsync(Invoice invoice);
    Task<IEnumerable<InvoiceHeader>> GetAllInvoiceHeadersAsync();
    Task<Invoice> GetInvoiceDetailsAsync(string invoiceId);
    Task<IEnumerable<Invoice>> GetUnprocessedInvoicesAsync();
    Task UpdateInvoiceAsync(Invoice invoice);
}