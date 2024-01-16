using Microsoft.EntityFrameworkCore;

namespace FirewoodAPI.Models
{
	public class FirewoodContext : DbContext
	{
        public FirewoodContext(DbContextOptions<FirewoodContext> options) : base(options) {}
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>(role =>
			{
				role.ToTable("rol");
				role.HasKey(r => r.RoleId);
				role.Property(r => r.RoleId).UseIdentityColumn();
				role.Property(r => r.Name).IsRequired();
			});

			modelBuilder.Entity<User>(user =>
			{
				user.ToTable("user");
				user.HasKey(u => u.UserId);
				user.Property(u => u.UserId).HasDefaultValueSql("NEWID()");
				user.Property(u => u.FullName).IsRequired().HasMaxLength(200);
				user.Property(u => u.Password).IsRequired();
				user.Property(u => u.Email).IsRequired();
				user.Property(u => u.IsActive).HasDefaultValue(true);
				user.HasMany(r => r.Roles)
				.WithMany(u => u.Users)
				.UsingEntity(ur => ur.ToTable("userRol"));
			});
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
            foreach (var item in ChangeTracker.Entries())
            {
                if(item.Entity is BaseModel entity)
				{
					switch(item.State)
					{
						case EntityState.Added:
							entity.CreatedAt = DateTime.Now;
							break;
						case EntityState.Modified:
							Entry(entity).Property(x => x.CreatedAt).IsModified = false;
							entity.UpdatedAt = DateTime.Now;
							break;
						default:
							break;
					}
				}
            }
            return base.SaveChangesAsync(cancellationToken);
		}

	}
}
