using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Reail_Shop_Backend.Controllers;
using Reail_Shop_Backend.Interfaces;
using Reail_Shop_Backend.DTOs;
using Reail_Shop_Backend.Models;
using System.Threading.Tasks;

namespace Reail_Shop_Backend.Tests
{
    public class InvoiceControllerTests
    {
        private Mock<IInvoiceService> _invoiceServiceMock;
        private InvoiceController _controller;

        [SetUp]
        public void Setup()
        {
            _invoiceServiceMock = new Mock<IInvoiceService>();
            _controller = new InvoiceController(null, _invoiceServiceMock.Object);
        }

        // Test for CreateInvoice method
        [Test]
        public async Task CreateInvoice_ValidDto_ReturnsOk()
        {
            
            var dto = new CreateInvoiceDto
            {
                
                TransactionDate = System.DateTime.Now,
                Items = new System.Collections.Generic.List<ItemInvoiceDto>()
        {
            new ItemInvoiceDto { ProductId = 1, Quantity = 2, Discount = 0 }
        }
            };

            var createdInvoice = new CustomerInvoice
            {
                InvoiceId = 1,
                TotalAmount = 100,
                BalanceAmount = 90,
                TransactionDate = dto.TransactionDate,
                ItemInvoice = new List<ItemInvoice>()
                {
                   new ItemInvoice
                   {
                    ProductId = 1,
                    Quantity = 2,
                    Discount = 0,
                    DiscountedPrice = 90,
                    TotalPrice = 100
                   }
                }
            };

            _invoiceServiceMock
                .Setup(s => s.CreateInvoiceAsync(dto))
                .ReturnsAsync(createdInvoice);
        
            var result = await _controller.CreateInvoice(dto);

           
            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);

       
        }

    }
}
