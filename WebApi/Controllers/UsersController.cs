using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Entities;
using WebApi.Enums;
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

		[HttpGet]
		public async Task<IActionResult> Get(long id)
		{
			var user = await _db.Users.FindAsync(id);

			var result = _mapper.Map<UserResponse>(user);

            result.Projects = _db.Projects.Include(x => x.Employees)
				.Where(p => p.Employees.Contains(user))
				.Select(x => _mapper.Map<ProjectResponseForUsers>(x)).ToList();

			return Ok(result);
		}

        [HttpGet]
        public async Task<IActionResult> GetAll(long companyId)
        {
			var users = await _db.Users
				.Where(x => x.CompanyId == companyId)
				.Select(x => _mapper.Map<UserResponse>(x))
				.ToListAsync();

            return Ok(users);
        }

        [HttpPost]
		public async Task<IActionResult> SignIn(SignInRequest req)
		{
			var user = await _db.Users.FirstOrDefaultAsync(x => x.Login == req.Login);

			if (user is null) return BadRequest("Неправильный логин или пароль!");

            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.Password)) return BadRequest("Неправильный логин или пароль!");

            var result = _mapper.Map<UserResponse>(user);

            result.Projects = _db.Projects.Include(x => x.Employees)
                .Where(p => p.Employees.Contains(user))
                .Select(x => _mapper.Map<ProjectResponseForUsers>(x)).ToList();

            return Ok(result);
        }

		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpRequest req)
		{
			var isLoginBusy = await _db.Users.AnyAsync(x => x.Login == req.Login);

			if (isLoginBusy) return BadRequest("Этот логин занят!");

			var newUser = _mapper.Map<User>(req);

			newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

			newUser.Role = Enums.RoleType.Employee;

			await _db.AddAsync(newUser);

			await _db.SaveChangesAsync();

            var result = _mapper.Map<UserResponse>(newUser);

            return Ok(result);
		}

		[HttpPut]
		public async Task<IActionResult> ToggleActivation(long userId, long executorId)
		{
			var user = await _db.Users.FindAsync(userId);

            var executor = await _db.Users.FindAsync(executorId);

			if(user is null || executor is null) return BadRequest();

			if(executor.Role is not Enums.RoleType.Admin) 
			{
				return BadRequest("Permission denied!");
			}

			user.IsActivated = !user.IsActivated;

			await _db.SaveChangesAsync();

            var result = _mapper.Map<UserResponse>(user);

            return Ok(result);
        }

		[HttpPut]
        public async Task<IActionResult> Update(UpdateUserRequest req)
		{
			var user = await _db.Users.FindAsync(req.Id);

			if(user is null) return BadRequest();

			user.Email = req.Email;
            user.Phone = req.Phone;
            user.Position = req.Position;

            await _db.SaveChangesAsync();

            var result = _mapper.Map<UserResponse>(user);

            return Ok(result);
		}

		[HttpPut]

		public async Task<IActionResult> ChangeRole(long userId, RoleType role)
		{
			var user = await _db.Users.FindAsync(userId);

			if(user is null) return BadRequest();

			user.Role = role;

			await _db.SaveChangesAsync();

            var result = _mapper.Map<UserResponse>(user);

            return Ok(result);
		}


        [HttpDelete]
		public async Task<IActionResult> Delete(long id)
		{
            var user = await _db.Users.FindAsync(id);

			if (user is null) return BadRequest();

			_db.Remove(user);

			await _db.SaveChangesAsync();

			return Ok();
        }
	}
}
