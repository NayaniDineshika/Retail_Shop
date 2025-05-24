using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reail_Shop_Backend.Data;
using Reail_Shop_Backend.Interfaces;

namespace Reail_Shop_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RetailDBContext _dbContext;

        public ProductController(RetailDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Get All Products
        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProductDetails()
        {
            var product = await _dbContext.Products.ToListAsync();
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }
    }
}
