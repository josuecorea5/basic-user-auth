using FirewoodAPI.Models;

namespace FirewoodAPI.Authentication
{
    public interface IJwtProvider
    {
        string GenerateJWT(User user);
    }
}
