using INVCAPP.API.Validations;
using INVCAPP.Core.DTOs;
using INVCAPP.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace INVCAPP.API.Controllers
{

    public class InvoiceController : CustomBaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        [ValidateInvoiceCreateDto]
        public async Task<IActionResult> AddInvoice(InvoiceCreateDto invoiceCreateDto)
        {
            var result = await _invoiceService.AddInvoiceAsync(invoiceCreateDto);
            return CreateActionResult(result);
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetAllInvoiceHeaders()
        {
            //Log.Information("Tüm fatura başlıklarını almak için GetAllInvoiceHeaders çağrıldı.");
            var result = await _invoiceService.GetAllInvoiceHeadersAsync();
            return CreateActionResult(result);
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoiceDetails(string invoiceId)
        {
            var result = await _invoiceService.GetInvoiceDetailsAsync(invoiceId);
            return CreateActionResult(result);
        }
    }

}
