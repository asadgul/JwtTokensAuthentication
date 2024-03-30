using JwtAuthe.IJwtServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthe.Models
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        public IDictionary<string,string> validusers=new Dictionary<string, string>() { { "test1","asadgul"},{"test2","talha" } };
        string key;
        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }
        public string AuthenticateUser(Users users)
        {
            if(!validusers.Any(x=>x.Key==users.UserName && x.Value == users.Password))
            {
                return null;
            }
            var token = new JwtSecurityTokenHandler();
            var tokenkey=Encoding.ASCII.GetBytes(key);
            var tokendecriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, users.UserName)
            }),
                Expires=DateTime.UtcNow.AddMinutes(10),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenkey),SecurityAlgorithms.HmacSha256Signature)
            
            };
            var createtoken = token.CreateToken(tokendecriptor);
            return token.WriteToken(createtoken);
        }
    }
}
