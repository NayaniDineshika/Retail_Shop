namespace Reail_Shop_Backend.Interfaces
{
    public interface IDiscountService
    {
        decimal CalculateDiscount(decimal unitPrice, int quantity, decimal discount);
    }
}
