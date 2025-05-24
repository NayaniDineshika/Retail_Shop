using Reail_Shop_Backend.Interfaces;
using Reail_Shop_Backend.Models;

namespace Reail_Shop_Backend.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal CalculateDiscount(decimal unitPrice, int quantity, decimal discount)
        {
            //Calculate the total price and discounted price per item
            var totalProductPrice = unitPrice * quantity;
            decimal discountedPrice = totalProductPrice * (1 - discount / 100);
            return discountedPrice;
        }
    }
}
