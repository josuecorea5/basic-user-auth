namespace FirewoodAPI.Models
{
	public class Role : BaseModel
	{
        public int RoleId { get; set; }
		public string? Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
