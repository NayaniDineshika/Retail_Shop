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

        public InvoiceService(RetailDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerInvoice> CreateInvoiceAsync (CreateInvoiceDto dto)
        {         
            var productsIds = dto.Items.Select(x => x.ProductId).ToList();
            var products = await _dbContext.Products.Where(p => productsIds.Contains(p.ProductId)).ToListAsync();

            var itemsInvoice = new List<ItemInvoice>();
            decimal totalAmount= 0;

            foreach (var itemDto in dto.Items)
            {
                var product = products.FirstOrDefault(p => p.ProductId == itemDto.ProductId);
                var totalProductPrice = product.UnitPrice * itemDto.Quantity;
                decimal discountedPrice = totalProductPrice * (1 - itemDto.Discount / 100);

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
                TotalAmount = itemsInvoice.Sum(i => i.TotalPrice),
                BalanceAmount = itemsInvoice.Sum(i => i.DiscountedPrice),
            };

            _dbContext.CustomerInvoices.Add(invoice);
            _dbContext.SaveChanges();
            return invoice;
        }
    }
}