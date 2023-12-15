using AutoMapper;
using BCrypt.Net;
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
	public class UsersController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _db;

		public UsersController(AppDbContext context, IMapper mapper)
		{
			_db = context;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpRequest req)
		{
			if (req is null) return BadRequest();

			var isLoginBusy = await _db.Users.AnyAsync(x => x.Login == req.Login);

			if (isLoginBusy) return BadRequest("Этот логин занят!");

			var newUser = _mapper.Map<User>(req);
			newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
			newUser.Role = Enums.RoleType.Employee;
			await _db.AddAsync(newUser);
			await _db.SaveChangesAsync();

			return Ok();
		}
	}
}
