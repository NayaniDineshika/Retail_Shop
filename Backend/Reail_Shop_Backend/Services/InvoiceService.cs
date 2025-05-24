using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reail_Shop_Backend.Data;
using Reail_Shop_Backend.DTOs;
using Reail_Shop_Backend.Interfaces;
using Reail_Shop_Backend.Models;

namespace Reail_Shop_Backend.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly RetailDBContext _dbContext;
        private readonly IDiscountService _discountService;

        public InvoiceService(RetailDBContext dbContext, IDiscountService discountService)
        {
            _dbContext = dbContext;
            _discountService = discountService;
        }

        public async Task<CustomerInvoice> CreateInvoiceAsync (CreateInvoiceDto dto)
        {         
            var productsIds = dto.Items.Select(x => x.ProductId).ToList();
            var products = await _dbContext.Products.Where(p => productsIds.Contains(p.ProductId)).ToListAsync();

            var itemsInvoice = new List<ItemInvoice>();
            decimal totalAmount= 0;

            foreach (var itemDto in dto.Items)
            {
                // Find the products by ID
                var product = products.FirstOrDefault(p => p.ProductId == itemDto.ProductId);

                //Calculate the total price and discounted price per item
                var discountedPrice = _discountService.CalculateDiscount(
                    product.UnitPrice, itemDto.Quantity, itemDto.Discount);

                var totalProductPrice = product.UnitPrice * itemDto.Quantity;

                itemsInvoice.Add(new ItemInvoice
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Discount = itemDto.Discount,
                    DiscountedPrice = discountedPrice,
                    TotalPrice = totalProductPrice,
                    
                });
            }

            var invoice = new CustomerInvoice
            {
                TransactionDate = dto.TransactionDate,
                ItemInvoice = itemsInvoice,
                //Calculate the total amount and balance amount
                TotalAmount = itemsInvoice.Sum(i => i.TotalPrice),
                BalanceAmount = itemsInvoice.Sum(i => i.DiscountedPrice),
            };

            _dbContext.CustomerInvoices.Add(invoice);
            _dbContext.SaveChanges();
            return invoice;
        }
    }
}