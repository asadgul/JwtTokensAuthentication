using JwtAuthe.Models;

namespace JwtAuthe.CustomJwt
{
    public class JwtCustomManager : IJwtCustomManager
    {
        public IDictionary<string, string> keyValuePairs => tokens;
        public IDictionary<string, string> tokens = new Dictionary<string, string>();
        public string AuthenticateUser(Users users)
        {
            var token=Guid.NewGuid().ToString();
            tokens.Add(token, users.UserName);
            return token;
        }
    }
}
