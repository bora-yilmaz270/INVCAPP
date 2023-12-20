using INVCAPP.Core.DTOs;

namespace INVCAPP.Core.Services;

public interface IInvoiceService
{
    Task<CustomResponseDto<NoContentDto>> AddInvoiceAsync(InvoiceCreateDto invoiceCreateDto);
    Task<CustomResponseDto<List<InvoiceHeaderDto>>> GetAllInvoiceHeadersAsync();
    Task<CustomResponseDto<InvoiceDto>> GetInvoiceDetailsAsync(string invoiceId);
}