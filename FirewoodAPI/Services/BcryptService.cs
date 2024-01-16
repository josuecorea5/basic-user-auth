namespace FirewoodAPI.Services
{
	public class BcryptService : IBcryptService
	{
		public string HashPassword(string password)
		{
			var passwordHash = BCrypt.Net.BCrypt.HashPassword(password, 11);
			return passwordHash;
		}

		public bool VerifyPassword(string password, string hashPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashPassword);
		}
	}
}
