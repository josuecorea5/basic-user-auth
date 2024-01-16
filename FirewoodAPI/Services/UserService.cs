using AutoMapper;
using FirewoodAPI.DTOs;
using FirewoodAPI.Models;
using FirewoodAPI.Repositories;

namespace FirewoodAPI.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IReadRepository<Role> _roleRepository;
		private readonly IMapper _mapper;
		private readonly IBcryptService _cryptService;
        public UserService(IUserRepository userRepository, IMapper mapper, IBcryptService bcryptService, IReadRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
			_mapper = mapper;
			_cryptService = bcryptService;
			_roleRepository = roleRepository;
        }

		public async Task<IEnumerable<UserDto>> GetAll()
		{
			var users = await _userRepository.GetAll();
			return users.Select(u => _mapper.Map<UserDto>(u));
		}
		public async Task<UserDto> GetById(Guid id)
		{
			var user = await _userRepository.GetById(id);

			if (user != null)
			{
				return _mapper.Map<UserDto>(user);
			}

			return null;
		}
		public async Task<UserDto> Add(UserInsertDto item)
		{
			var user = _mapper.Map<User>(item);
			var userRole = await _roleRepository.GetById((int)Roles.Usuario);
			user.Password = _cryptService.HashPassword(item.Password);
			user.Roles = new List<Role> { userRole };
			await _userRepository.Add(user);
			await _userRepository.Save();
			var userDto = _mapper.Map<UserDto>(user);

			return userDto;
		}

		public bool ValidatePassword(string password, string hashPassword)
			=> _cryptService.VerifyPassword(password, hashPassword);

		public User GetUserByEmail(string email)
			=> _userRepository.GetUserByEmail(email);

		public async Task<UserDto> Update(Guid id, UserUpdateDto item)
		{
			var user = await _userRepository.GetById(id);

			if (user is not null)
			{
				user.FullName = item.FullName;
				user.Email = item.Email;
				user.Password = _cryptService.HashPassword(item.Password);

				_userRepository.Update(user);
				await _userRepository.Save();

				var userDto = _mapper.Map<UserDto>(user);

				return userDto;
			}

			return null;
		}

		public async Task<UserDto> DeleteById(Guid id)
		{
			var user = await _userRepository.GetById(id);

			if (user is not null)
			{
				var userDto = _mapper.Map<UserDto>(user);
				_userRepository.Delete(user);
				await _userRepository.Save();

				return userDto;
			}

			return null;
		}
	}
}
