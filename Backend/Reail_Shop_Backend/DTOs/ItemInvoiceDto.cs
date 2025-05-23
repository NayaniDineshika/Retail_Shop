namespace Reail_Shop_Backend.DTOs
{
    public class ItemInvoiceDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
