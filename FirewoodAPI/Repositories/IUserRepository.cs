using FirewoodAPI.Models;

namespace FirewoodAPI.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		User GetUserByEmail(string email);
	}
}
