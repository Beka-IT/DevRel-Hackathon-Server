using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReferencesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public ReferencesController(AppDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var references = _db.References.ToList();

            return Ok(references);
        }
    }
}
