using FirewoodAPI.DTOs;
using FirewoodAPI.Models;

namespace FirewoodAPI.Services
{
	public interface IUserService : ICommonService<UserDto, UserInsertDto, UserUpdateDto>
	{
		bool ValidatePassword(string password, string hashPassword);
		User GetUserByEmail(string email);
	}
}
