using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public ProjectsController(AppDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var project = await _db.Projects.FindAsync(id);

            return Ok(project);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long companyId)
        {
            var projects = await _db.Projects.Where(x => x.CompanyId == companyId).ToListAsync();

            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProjectRequest req)
        {
            var newProject = _mapper.Map<Project>(req);

            await _db.AddAsync(newProject);

            await _db.SaveChangesAsync();

            return Ok(newProject);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectRequest req)
        {
            var project = await _db.Projects.FindAsync(req.Id);

            if (project is null) return BadRequest();

            project.Title = req.Title;

            project.Description = req.Description;

            project.MaintainerId = req.MaintainerId;

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project is null) return BadRequest();

            _db.Remove(project);

            await _db.SaveChangesAsync();

            return Ok(project);
        }
    }
}
