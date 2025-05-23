using System.ComponentModel.DataAnnotations;

namespace Reail_Shop_Backend.Models
{
    public class CustomerInvoice
    {
        [Key]
        public int InvoiceId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal BalanceAmount { get; set; }

        public required ICollection<ItemInvoice> ItemInvoice { get; set; }
    }
}
