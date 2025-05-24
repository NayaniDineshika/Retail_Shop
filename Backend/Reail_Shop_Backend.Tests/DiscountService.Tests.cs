using NUnit.Framework;
using Reail_Shop_Backend.Services;

namespace Reail_Shop_Backend.Tests
{
    public class DiscountServiceTests
    {
        private DiscountService _discountService;

        [SetUp]
        public void Setup()
        {
            _discountService = new DiscountService();
        }

        //CalculateDiscount With 10% Discount
        [Test]
        public void CalculateDiscount_WithDiscount_ReturnsCorrectAmount()
        {
            // Arrange
            decimal unitPrice = 100m;
            int quantity = 2;
            decimal discount = 10;

            // Act
            decimal result = _discountService.CalculateDiscount(unitPrice, quantity, discount);

            // Assert
            Assert.AreEqual(180m, result);
        }

        //Calculate Discount WithZeroDiscount
        [Test]
        public void CalculateDiscount_WithZeroDiscount_ReturnsFullAmount()
        {
            decimal result = _discountService.CalculateDiscount(50m, 3, 0);
            Assert.AreEqual(150m, result);
        }

    }
}

