using Microsoft.AspNetCore.Authorization;

namespace FirewoodAPI.Authentication
{
	public class HasPermissionAttribute : AuthorizeAttribute
	{
		public HasPermissionAttribute(Permission permission) : base(permission.ToString()) { }
	}

}
