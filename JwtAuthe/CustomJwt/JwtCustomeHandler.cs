using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace JwtAuthe.CustomJwt
{
    public class BasicAuthenticationScheme : AuthenticationSchemeOptions
    {

    }
    public class JwtCustomeHandler : AuthenticationHandler<BasicAuthenticationScheme>
    {
        private readonly IJwtCustomManager _jwtcustomemanager;
        public JwtCustomeHandler(IOptionsMonitor<BasicAuthenticationScheme> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,IJwtCustomManager jwtCustomManager) : base(options, logger, encoder, clock)
        {
            _jwtcustomemanager = jwtCustomManager;
        }

        protected override async  Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("UnAuthorized");
            }
            string authorizationHeader = Request.Headers["Authorization"];
            string token = authorizationHeader.Substring("bearer".Length).Trim();            
           return   ValidateToken(token);

        }
        public AuthenticateResult ValidateToken(string token)
        {
            var validatetoken = _jwtcustomemanager.keyValuePairs.FirstOrDefault(x => x.Key == token);
            if (validatetoken.Key == null)
            {
                return AuthenticateResult.Fail("UnAuthoried");
            }
            var claim = new List<Claim>() { new Claim(ClaimTypes.Name, validatetoken.Value) };
            var identity=new ClaimsIdentity(claim,Scheme.Name);
            var principal=new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
