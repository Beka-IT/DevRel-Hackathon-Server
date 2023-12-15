using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Xml.Linq;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Enums;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public TasksController(AppDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var task = await _db.Tasks.FindAsync(id);

            var response = _mapper.Map<TaskResponse>(task);

            response.Comments = await _db.Comments
                .Where(x => x.TaskId == id)
                .Select(x => _mapper.Map<CommentResponse>(x))
                .ToListAsync();

            foreach (var comment in response.Comments)
            {
                var sender = await _db.Users.FirstOrDefaultAsync(x => x.Id == comment.SenderId);

                comment.Sender = _mapper.Map<UserResponse>(sender);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long projectId)
        {
            var tasks = await _db.Tasks.Where(x => x.ProjectId == projectId).ToListAsync();

            var responses = tasks.Where(x => x.ProjectId == projectId)
                .Select(x => _mapper.Map<TaskResponse>(x))
                .ToList();

            return Ok(responses);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateTaskRequest req)
        {
            var newTask = _mapper.Map<Entities.Task>(req);

            await _db.AddAsync(newTask);

            await _db.SaveChangesAsync();

            var response = _mapper.Map<TaskResponse>(newTask);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeStatus(ChangeTaskStatusRequest req)
        {
            var task = await _db.Tasks.FindAsync(req.Id);

            if (task is null) return BadRequest();

            task.Status = req.Status;
            
            task.ExecutorId = req.ExecutorId ?? task.ExecutorId;

            await _db.SaveChangesAsync();

            var response = _mapper.Map<TaskResponse>(task);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTaskRequest req)
        {
            var task = await _db.Tasks.FindAsync(req.Id);

            if (task is null) return BadRequest();

            task.Title = req.Title;

            task.Description = req.Description;

            task.ExecutorId = req.ExecutorId;

            task.Difficulty = req.Difficulty;

            task.DeadLine = req.DeadLine;

            await _db.SaveChangesAsync();

            var response = _mapper.Map<TaskResponse>(task);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var task = await _db.Tasks.FindAsync(id);

            if (task is null) return BadRequest();

            _db.Remove(task);

            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
