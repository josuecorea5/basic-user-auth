using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirewoodAPI.Models
{
	public class User : BaseModel
	{
		public Guid UserId { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Role> Roles { get; set; }
    }
}
