using AutoMapper;
using INVCAPP.Core.DTOs;
using INVCAPP.Core.Models;
using INVCAPP.Core.Repositories;
using INVCAPP.Core.Services;

namespace INVCAPP.Service.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IMapper _mapper;

    public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<NoContentDto>> AddInvoiceAsync(InvoiceCreateDto invoiceCreateDto)
    {
        var invoice = _mapper.Map<Invoice>(invoiceCreateDto);
        await _invoiceRepository.AddInvoiceAsync(invoice);
        return CustomResponseDto<NoContentDto>.Success(204);
    }

    public async Task<CustomResponseDto<List<InvoiceHeaderDto>>> GetAllInvoiceHeadersAsync()
    {
        var invoiceHeaders = await _invoiceRepository.GetAllInvoiceHeadersAsync();
        var invoiceHeadersDto = _mapper.Map<List<InvoiceHeaderDto>>(invoiceHeaders);
        return CustomResponseDto<List<InvoiceHeaderDto>>.Success(200, invoiceHeadersDto);
    }

    public async Task<CustomResponseDto<InvoiceDto>> GetInvoiceDetailsAsync(string invoiceId)
    {
        var invoice = await _invoiceRepository.GetInvoiceDetailsAsync(invoiceId);
        if (invoice == null)
        {
            return CustomResponseDto<InvoiceDto>.Fail(404, "Invoice not found.");
        }
        var invoiceDetailDto = _mapper.Map<InvoiceDto>(invoice);
        return CustomResponseDto<InvoiceDto>.Success(200, invoiceDetailDto);
    }
}