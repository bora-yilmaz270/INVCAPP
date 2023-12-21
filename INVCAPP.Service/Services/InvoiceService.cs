using AutoMapper;
using INVCAPP.Core.DTOs;
using INVCAPP.Core.Models;
using INVCAPP.Core.Repositories;
using INVCAPP.Core.Services;
using Microsoft.AspNetCore.Http;
using Serilog;

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
        try
        {
            var invoice = _mapper.Map<Invoice>(invoiceCreateDto);

            invoice.IsProcessed = new Random().Next(0, 2) > 0;

            await _invoiceRepository.AddInvoiceAsync(invoice);

            Log.Information("Fatura başarıyla eklendi: {@Invoice}", invoice);

            return CustomResponseDto<NoContentDto>.Success(201);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Fatura ekleme işlemi sırasında bir hata meydana geldi: {@InvoiceCreateDto}", invoiceCreateDto);
            var userFriendlyErrorMessage = "Bir sorun oluştu. Lütfen daha sonra tekrar deneyiniz.";
            var errorResponse = CustomResponseDto<NoContentDto>.Fail(
                StatusCodes.Status500InternalServerError,
                new List<string> { userFriendlyErrorMessage }
            );
            return errorResponse;
        }
    }

    public async Task<CustomResponseDto<List<InvoiceHeaderDto>>> GetAllInvoiceHeadersAsync()
    {
        try
        {
            var invoiceHeaders = await _invoiceRepository.GetAllInvoiceHeadersAsync();
            var invoiceHeadersDto = _mapper.Map<List<InvoiceHeaderDto>>(invoiceHeaders);
            Log.Information("Tüm fatura başlıkları başarıyla alındı.");
            return CustomResponseDto<List<InvoiceHeaderDto>>.Success(200, invoiceHeadersDto);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Tüm fatura başlıklarını alma işlemi sırasında bir hata meydana geldi.");
            var userFriendlyErrorMessage = "Bir sorun oluştu. Lütfen daha sonra tekrar deneyiniz.";
            var errorResponse = CustomResponseDto<List<InvoiceHeaderDto>>.Fail(
                StatusCodes.Status500InternalServerError,
                new List<string> { userFriendlyErrorMessage }
            );
            return errorResponse;
        }
    }

    public async Task<CustomResponseDto<InvoiceDto>> GetInvoiceDetailsAsync(string invoiceId)
    {
        try
        {
            var invoice = await _invoiceRepository.GetInvoiceDetailsAsync(invoiceId);
            if (invoice == null)
            {
                Log.Warning("Fatura bulunamadı. Fatura ID: {InvoiceId}", invoiceId);
                return CustomResponseDto<InvoiceDto>.Fail(404, "Invoice not found.");
            }
            var invoiceDetailDto = _mapper.Map<InvoiceDto>(invoice);
            Log.Information("Fatura detayları başarıyla alındı. Fatura ID: {InvoiceId}", invoiceId);
            return CustomResponseDto<InvoiceDto>.Success(200, invoiceDetailDto);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Fatura detayları alma işlemi sırasında bir hata meydana geldi. Fatura ID: {InvoiceId}", invoiceId);
            var userFriendlyErrorMessage = "Bir sorun oluştu. Lütfen daha sonra tekrar deneyiniz.";
            var errorResponse = CustomResponseDto<InvoiceDto>.Fail(
                StatusCodes.Status500InternalServerError,
                new List<string> { userFriendlyErrorMessage }
            );

            return errorResponse;
        }
    }
    public async Task<IEnumerable<Invoice>> GetUnprocessedInvoicesAsync()
    {
        try
        {
            var invoices = await _invoiceRepository.GetUnprocessedInvoicesAsync();
            Log.Information("İşlenmemiş faturalar başarıyla getirildi. Toplam Fatura Sayısı: {InvoiceCount}", invoices.Count());
            return invoices;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "İşlenmemiş faturaları getirme işlemi sırasında bir hata meydana geldi.");
            return Enumerable.Empty<Invoice>();
        }
    }
    public async Task UpdateInvoiceAsync(Invoice invoice)
    {
        try
        {
            await _invoiceRepository.UpdateInvoiceAsync(invoice);
            Log.Information("Fatura başarıyla güncellendi: {@Invoice}", invoice);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Fatura güncelleme işlemi sırasında bir hata meydana geldi: {@Invoice}", invoice);
        }
    }
}