namespace INVCAPP.Core.DTOs;

public class InvoiceLineCreateDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string UnitCode { get; set; }
    public decimal UnitPrice { get; set; }
}