using INVCAPP.Core.DTOs;
using INVCAPP.Core.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddInvoice(InvoiceCreateDto invoiceCreateDto)
        {
            var result = await _invoiceService.AddInvoiceAsync(invoiceCreateDto);
            return CreateActionResult(result);
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetAllInvoiceHeaders()
        {
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
