using Microsoft.EntityFrameworkCore;
using Reail_Shop_Backend.Data;
using Reail_Shop_Backend.DTOs;
using Reail_Shop_Backend.Models;
using Reail_Shop_Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;
using Moq;
using Reail_Shop_Backend.Interfaces;


namespace Reail_Shop_Backend.Tests
{
    public class InvoiceServiceTests
    {
        private RetailDBContext _dbContext;
        private InvoiceService _invoiceService;
        private Mock<IDiscountService> _discountServiceMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RetailDBContext>()
                .UseInMemoryDatabase(databaseName: "RetailTestDB")
            .Options;

            _dbContext = new RetailDBContext(options);
           
            _dbContext.Products.Add(new Product
            {
                ProductId = 1,
                ProductName = "Test Product",
                UnitPrice = 100,
                ItemInvoice = new List<ItemInvoice>()
            });

            _dbContext.SaveChanges();

            _discountServiceMock = new Mock<IDiscountService>();
            _discountServiceMock.Setup(ds => ds.CalculateDiscount(100,2,10))
                .Returns(90 *2);
            _invoiceService = new InvoiceService(_dbContext, _discountServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        // Test for CreateInvoiceAsync method
        // This test checks if the CreateInvoiceAsync method correctly creates an invoice
        [Test]
        public async Task CreateInvoiceAsync_ValidDto_CreatesInvoiceCorrectly()
        {
            // Arrange
            var dto = new CreateInvoiceDto
            {
                TransactionDate = DateTime.Now,
                Items = new List<ItemInvoiceDto>
                {
                    new ItemInvoiceDto
                    {
                        ProductId = 1,
                        Quantity = 2,
                        Discount = 10
                    }
                }
            };

            var result = await _invoiceService.CreateInvoiceAsync(dto);

           
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.TotalAmount);
            Assert.AreEqual(180, result.BalanceAmount);
            Assert.AreEqual(1, result.ItemInvoice.Count);
        }

    }
}
