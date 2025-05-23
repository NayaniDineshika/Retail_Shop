namespace Reail_Shop_Backend.DTOs
{
    public class CreateInvoiceDto
    {
        public DateTime TransactionDate { get; set; }
        public List<ItemInvoiceDto> Items { get; set; }
    }
}
