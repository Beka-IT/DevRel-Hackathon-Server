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
            var comment = await _db.Comments.FirstOrDefaultAsync(x => x.Id == id);

            var response = _mapper.Map<CommentResponse>(comment);

            var sender = await _db.Users.FirstOrDefaultAsync(x => x.Id == comment.SenderId);

            response.Sender = _mapper.Map<UserResponse>(sender);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(long taskId)
        {
            var comments = await _db.Comments
                .Where(x => x.TaskId == taskId)
                .Select(x => _mapper.Map<CommentResponse>(x))
                .ToListAsync();

            foreach (var comment in comments)
            {
                var sender = await _db.Users.FirstOrDefaultAsync(x => x.Id == comment.SenderId);

                comment.Sender = _mapper.Map<UserResponse>(sender);
            }

            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateCommentRequest req)
        {
            var newComment = _mapper.Map<Comment>(req);

            await _db.AddAsync(newComment);

            await _db.SaveChangesAsync();

            var response = _mapper.Map<CommentResponse>(newComment);
            
            var sender = await _db.Users.FirstOrDefaultAsync(x => x.Id == newComment.SenderId);

            response.Sender = _mapper.Map<UserResponse>(sender);

            return Ok(response);
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
