namespace INVCAPP.Core.DTOs
{
    public class InvoiceDto
    {
        public InvoiceHeaderDto InvoiceHeader { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
    }
}
