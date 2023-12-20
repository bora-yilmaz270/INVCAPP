namespace INVCAPP.Core.DTOs
{
    public class InvoiceDto: BaseDto
    {
        //public int InvoiceHeaderId { get; set; }
        public InvoiceHeaderDto InvoiceHeader { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
        //public bool IsProcessed { get; set; }
    }
}
