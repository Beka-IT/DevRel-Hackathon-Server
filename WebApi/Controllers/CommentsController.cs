using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public CommentsController(AppDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var comment = await _db.Comments.FindAsync(id);

            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long taskId)
        {
            var comments = await _db.Comments.Where(x => x.TaskId == taskId).ToListAsync();

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCommentRequest req)
        {
            var newTask = _mapper.Map<Comment>(req);

            await _db.AddAsync(newTask);

            await _db.SaveChangesAsync();

            return Ok(newTask);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            var comment = await _db.Comments.FindAsync(id);

            if (comment is null) return BadRequest();

            _db.Remove(comment);

            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
