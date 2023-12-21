using System.ComponentModel.DataAnnotations.Schema;

namespace INVCAPP.Core.Models
{
    public class Invoice : BaseEntity
    {
        public int InvoiceHeaderId { get; set; }

        [ForeignKey(nameof(InvoiceHeaderId))]
        public InvoiceHeader InvoiceHeader { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsEmailSent { get; set; }

    }
}
