using INVCAPP.Core.DTOs;
using INVCAPP.Core.Models;

namespace INVCAPP.Core.Services;

public interface IInvoiceService
{
    Task<CustomResponseDto<NoContentDto>> AddInvoiceAsync(InvoiceCreateDto invoiceCreateDto);
    Task<CustomResponseDto<List<InvoiceHeaderDto>>> GetAllInvoiceHeadersAsync();
    Task<CustomResponseDto<InvoiceDto>> GetInvoiceDetailsAsync(string invoiceId);
    Task<IEnumerable<Invoice>> GetUnprocessedInvoicesAsync();
    Task UpdateInvoiceAsync(Invoice invoice);
}