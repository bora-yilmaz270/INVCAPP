namespace INVCAPP.Core.Models;

public class InvoiceHeader : BaseEntity
{
    public string InvoiceId { get; set; }
    public string SenderTitle { get; set; }
    public string ReceiverTitle { get; set; }
    public string Date { get; set; }
    public string Email { get; set; }
        
}