using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CompaniesController(AppDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var company = await _db.Companies.FindAsync(id);

            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var company = await _db.Companies.ToListAsync();

            return Ok(company);
        }
    }
}
