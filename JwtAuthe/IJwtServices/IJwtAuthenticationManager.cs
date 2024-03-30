using JwtAuthe.Models;

namespace JwtAuthe.IJwtServices
{
    public interface IJwtAuthenticationManager
    {
        public string AuthenticateUser(Users users);
    }
}
