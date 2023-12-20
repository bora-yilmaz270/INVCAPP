namespace INVCAPP.Core.DTOs;

public class InvoiceLineDto: BaseDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string UnitCode { get; set; }
    public decimal UnitPrice { get; set; }
    public int InvoiceId { get; set; }
}