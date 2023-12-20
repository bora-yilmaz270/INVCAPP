using INVCAPP.Core.Services;

namespace INVCAPP.EmailService
{
    public  class EmailService:IEmailService
    {
        private readonly IInvoiceService _invoiceService;
        public EmailService(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        public async Task ProcessAndSendEmailForUnprocessedInvoices()
        {
            var unprocessedInvoices = await _invoiceService.GetUnprocessedInvoicesAsync();
            foreach (var invoice in unprocessedInvoices)
            {
                var emailBody = $"{invoice.InvoiceLines.Count} kalem ürün içeren {invoice.InvoiceHeader.InvoiceId} nolu faturanız başarıyla işlenmiştir.";
                await SendEmailAsync(invoice.InvoiceHeader.Email, "Fatura İşleme Bildirimi", emailBody);
                //Faturayı işlenmiş olarak işaretle
                invoice.IsProcessed = true;
                invoice.IsEmailSent=true;
                await _invoiceService.UpdateInvoiceAsync(invoice);
            }
        }
        private async Task SendEmailAsync(string to, string subject, string body)
        {
            Console.WriteLine("Mail Gitti");
        }
    }
}
