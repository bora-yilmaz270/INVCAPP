using INVCAPP.Core.Models;
using INVCAPP.Core.Repositories;
using Microsoft.EntityFrameworkCore;
namespace INVCAPP.Repository.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
    {
        await _context.Invoices.AddAsync(invoice);
        await _context.SaveChangesAsync();
        return invoice;
    }
    public async Task<IEnumerable<InvoiceHeader>> GetAllInvoiceHeadersAsync()
    {
        return await _context.InvoiceHeaders.ToListAsync();
    }
    public async Task<Invoice> GetInvoiceDetailsAsync(string invoiceId)
    {
        return await _context.Invoices
            .Include(i => i.InvoiceHeader)
            .Include(i => i.InvoiceLines)
            .SingleOrDefaultAsync(i => i.InvoiceHeader.InvoiceId == invoiceId);
    }
    public async Task<IEnumerable<Invoice>> GetUnprocessedInvoicesAsync()
    {
        return await _context.Invoices
            .Include(i => i.InvoiceHeader)
            .Include(i => i.InvoiceLines)
            .Where(i => !i.IsProcessed)
            .ToListAsync();
    }
    public async Task UpdateInvoiceAsync(Invoice invoice)
    {
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
    }
}