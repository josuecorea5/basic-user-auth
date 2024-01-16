using FirewoodAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirewoodAPI.Repositories
{
	public class RoleRepository : IReadRepository<Role>
	{
		private FirewoodContext _context;
        public RoleRepository(FirewoodContext firewoodContext)
        {
            _context = firewoodContext;
        }

		public async Task<IEnumerable<Role>> GetAll()
			=> await _context.Roles.ToListAsync();

		public async Task<Role> GetById<T>(T roleName)
		{
			var userRole = await _context.Roles.FindAsync(roleName);
			return userRole;
		}
	}
}
