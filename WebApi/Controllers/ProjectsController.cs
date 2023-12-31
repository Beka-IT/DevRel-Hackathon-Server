﻿using AutoMapper;
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
            var project = await _db.Projects.Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == id);

            var result = _mapper.Map<ProjectResponse>(project);

            result.Members = project.Employees
                .Select(x => _mapper.Map<UserResponseForProject>(x)).ToList();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long companyId)
        {
            var projects = await _db.Projects
                .Where(x => x.CompanyId == companyId)
                .Select(x => _mapper.Map<ProjectResponse>(x))
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(long userId)
        {
            var user = _db.Users.Find(userId);

            var projects = await _db.Projects
                .Where(x => x.Employees.Contains(user))
                .Select(x => _mapper.Map<ProjectResponse>(x))
                .ToListAsync();

            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProjectRequest req)
        {
            var newProject = _mapper.Map<Project>(req);

            var companyOwnerId = _db.Companies.Find(req.CompanyId).OwnerId;

            var companyOwner = await _db.Users.FindAsync(companyOwnerId);

            var maintainer = await _db.Users.FindAsync(req.MaintainerId);

            newProject.Employees = new List<User> { maintainer, companyOwner };

            await _db.AddAsync(newProject);

            await _db.SaveChangesAsync();

            var result = _mapper.Map<ProjectResponse>(newProject);

            result.Members = newProject.Employees
                .Select(x => _mapper.Map<UserResponseForProject>(x)).ToList();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectRequest req)
        {
            var project = await _db.Projects.Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == req.Id);

            if (project is null) return BadRequest();

            project.Title = req.Title;

            project.Description = req.Description;

            project.MaintainerId = req.MaintainerId;

            await _db.SaveChangesAsync();

            var result = _mapper.Map<ProjectResponse>(project);

            result.Members = project.Employees
                .Select(x => _mapper.Map<UserResponseForProject>(x)).ToList();

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var project = await _db.Projects.FindAsync(id);

            if (project is null) return BadRequest();

            _db.Remove(project);

            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(long employeeId, long projectId)
        {
            var employee = await _db.Users.FindAsync(employeeId);
            var project = await _db.Projects
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if(employee is null || project is null) return BadRequest();

            project.Employees.Add(employee);

            await _db.SaveChangesAsync();

            var result = _mapper.Map<ProjectResponse>(project);

            result.Members = project.Employees
                .Select(x => _mapper.Map<UserResponseForProject>(x)).ToList();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> DeleteMember(long employeeId, long projectId)
        {
            var employee = await _db.Users.FindAsync(employeeId);
            var project = await _db.Projects
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(x => x.Id == projectId);

            if (employee is null || project is null) return BadRequest();

            project.Employees.Remove(employee);

            await _db.SaveChangesAsync();

            var result = _mapper.Map<ProjectResponse>(project);

            result.Members = project.Employees
                .Select(x => _mapper.Map<UserResponseForProject>(x)).ToList();

            return Ok(result);
        }
    }
}
