using JwtAuth.DataAccess;
using JwtAuth.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JwtAuth.Controllers
{
    public class ProductsController : JwtAuthControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
