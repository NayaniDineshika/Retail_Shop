using System.ComponentModel.DataAnnotations;

namespace Reail_Shop_Backend.Models
{
    public class Product
    {
        [Key]
        public int ProductId {  get; set; }
        public required string ProductName { get; set; }
        public  decimal UnitPrice { get; set; }

        public required ICollection<ItemInvoice> ItemInvoice { get; set; }
    }
}
