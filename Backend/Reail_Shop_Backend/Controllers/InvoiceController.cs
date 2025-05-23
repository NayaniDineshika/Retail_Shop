using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reail_Shop_Backend.Data;
using Reail_Shop_Backend.DTOs;
using Reail_Shop_Backend.Models;
using Reail_Shop_Backend.Services;
using Reail_Shop_Backend.Interfaces;

namespace Reail_Shop_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly RetailDBContext _dbContext;
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(RetailDBContext dbContext, IInvoiceService invoiceService)
        {
            _dbContext = dbContext;
            _invoiceService = invoiceService;
        }

        
        [HttpPost("createInvoice")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CustomerInvoice createdInvoice = await _invoiceService.CreateInvoiceAsync(dto);
                return Ok(new
                {
                    Message = "Invoice created successfully",
                    createdInvoice.InvoiceId,
                    createdInvoice.TotalAmount,
                    createdInvoice.BalanceAmount,
                    createdInvoice.TransactionDate
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving invoice: {ex.Message} - Inner: {ex.InnerException?.Message}");
            }
        }

     

        [HttpGet("itemInvoice/{invoiceId}")]
        public async Task<IActionResult> GetItemInvoiceById(int invoiceId)
        {
            var itemInvoices = await _dbContext.ItemInvoices
                .Where(i => i.InvoiceId == invoiceId)
                .Include(i => i.Product) 
                .ToListAsync();

            if (itemInvoices == null || itemInvoices.Count == 0)
            {
                return NotFound("Invoice not found or no items in invoice.");
            }

        
            var result = itemInvoices.Select(i => new
            {
                i.ProductId,
                i.Product.ProductName,
                i.Product.UnitPrice,
                i.Quantity,
                i.Discount,
                i.TotalPrice,
                i.DiscountedPrice
            });

            return Ok(result);
        }

    }
}
