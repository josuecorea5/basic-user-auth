using FirewoodAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirewoodAPI.Repositories
{
	public class UserRepository : IUserRepository
	{
		private FirewoodContext _context;

        public UserRepository(FirewoodContext firewoodContext)
        {
            _context = firewoodContext;
        }
        public async Task<IEnumerable<User>> GetAll()
		{
			return await _context.Users.Include(r => r.Roles).ToListAsync();
		}

		public async Task<User> GetById<T>(T id)
		{
			return await _context.Users.FindAsync(id);
		}
		public async Task Add(User user)
		{
			await _context.Users.AddAsync(user);
		} 

		public void Update(User user)
		{
			_context.Users.Attach(user);
			_context.Entry(user).State = EntityState.Modified;
		}

		public void Delete(User entity)
		{
			_context.Users.Remove(entity);
		}

		public async Task Save()
			=> await _context.SaveChangesAsync();

		public User GetUserByEmail(string email)
			=> _context.Users.Where(u => u.Email == email).Include(r => r.Roles).FirstOrDefault();
	}
}
