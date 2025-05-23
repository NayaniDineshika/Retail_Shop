using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reail_Shop_Backend.Models
{
    public class ItemInvoice
    {
        [Key]
        public int ItemInvoiceId { get; set; }

        [ForeignKey("CustomerInvoice")]
        public int InvoiceId { get; set; }
        public CustomerInvoice CustomerInvoice { get; set; }

        public int ProductId { get; set; }
        public  Product  Product { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
