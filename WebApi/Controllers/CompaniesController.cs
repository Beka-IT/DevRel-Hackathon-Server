using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public CompaniesController(AppDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var company = await _db.Companies
                .Include(x => x.Employees)
                .Include(x => x.Projects)
                .FirstOrDefaultAsync(x => x.Id == id);

            var result = _mapper.Map<CompanyResponse>(company);

            result.Employees = company.Employees.
                Select(x => _mapper.Map<UserResponse>(x)).ToList();

            result.Projects = company.Projects.
                Select(x => _mapper.Map<ProjectResponse>(x)).ToList();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var company = await _db.Companies.ToListAsync();

            return Ok(company);
        }
    }
}
