using FirewoodAPI.Authentication;
using FirewoodAPI.DTOs;
using FirewoodAPI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirewoodAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IValidator<UserInsertDto> _userValidator;
		private readonly IValidator<UserLoginDto> _userLoginValidator;
		private readonly IValidator<UserUpdateDto> _userUpdateValidator;
		private readonly IJwtProvider _jwtService;

		public UsersController(IUserService userService, IValidator<UserInsertDto> userValidator, IValidator<UserLoginDto> loginValidator, IJwtProvider jwtService, IValidator<UserUpdateDto> userUpdateValidator)
		{
			_userService = userService;
			_userValidator = userValidator;
			_userLoginValidator = loginValidator;
			_jwtService = jwtService;
			_userUpdateValidator = userUpdateValidator;
		}

		[Authorize(Roles = "Usuario")]
		[HttpGet]
		public async Task<IEnumerable<UserDto>> GetAll() =>
			await _userService.GetAll();

		[HttpGet("{id}")]
		public async Task<ActionResult<UserDto>> GetById(Guid id)
		{
			var user = await _userService.GetById(id);

			if( user == null)
			{
				return NotFound();
			}

			return Ok(user);
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Add(UserInsertDto user)
		{
			var validationResult = _userValidator.Validate(user);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
			}

			return await _userService.Add(user);
		}

		[HttpPost("login")]
		public IActionResult Login(UserLoginDto userLoginDto)
		{
			var validationResult = _userLoginValidator.Validate(userLoginDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
			}

			var user = _userService.GetUserByEmail(userLoginDto.Email);

			if (user != null)
			{
				var validatePassword = _userService.ValidatePassword(userLoginDto.Password, user.Password);

				if (!validatePassword)
				{
					return BadRequest(new { message = "email or password is incorrect" });
				}

				string token = _jwtService.GenerateJWT(user);

				return Ok(new { token });
			}

			return NotFound(new { message = "User not found" });
		}

		[Authorize(Roles = "Usuario")]
		[HttpPut("{id}")]
		public async Task<ActionResult<UserDto>> Update(Guid id, UserUpdateDto userUpdateDto)
		{
			var validationResult = await _userUpdateValidator.ValidateAsync(userUpdateDto);

			if(!validationResult.IsValid)
			{
				return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage}));
			}

			var user = await _userService.Update(id, userUpdateDto);

			return user == null ? NotFound(new { message = "User not found" }) : Ok(user);
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<UserDto>> Delete(Guid id)
		{
			var user = await _userService.DeleteById(id);

			return user == null ? BadRequest(new { message = "User not foudd"}) : Ok(user);
		}
    }
}
