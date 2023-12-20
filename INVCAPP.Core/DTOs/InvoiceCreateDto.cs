namespace INVCAPP.Core.DTOs;

public class InvoiceCreateDto 
{
    public InvoiceHeaderCreateDto InvoiceHeader { get; set; }
    public List<InvoiceLineCreateDto> InvoiceLines { get; set; }
        
}