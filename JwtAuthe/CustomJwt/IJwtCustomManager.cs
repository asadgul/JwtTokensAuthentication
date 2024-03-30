using JwtAuthe.Models;

namespace JwtAuthe.CustomJwt
{
    public interface IJwtCustomManager
    {
        public string AuthenticateUser(Users users);
        public IDictionary<string, string> keyValuePairs { get;}
    }
}
