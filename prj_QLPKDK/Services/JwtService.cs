using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using prj_QLPKDK.Data;
using prj_QLPKDK.Models.Response;
using prj_QLPKDK.Models.Resquest;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace prj_QLPKDK.Services
{
    public class JwtService
    {
        private readonly WebContext _context;
        private readonly IConfiguration _configuaration;

        public JwtService(WebContext hospitalContext, IConfiguration configuaration)
        {
            _context = hospitalContext;
            _configuaration = configuaration;
        }
        public async Task<LoginResponseModel?> Authenticate(LoginRequestModel req)
        {
            if (string.IsNullOrWhiteSpace(req.UserName) || string.IsNullOrWhiteSpace(req.Password))
            {
                return null;
            }
            var UserAccount = await _context.Users.FirstOrDefaultAsync(x => x.Username == req.UserName);
           
            if (UserAccount != null && UserAccount.Password == req.Password)
            {

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,_configuaration["JwtConfig:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("UserName",req.UserName.ToString()),
                    new Claim(ClaimTypes.Role, UserAccount.Role.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuaration["JwtConfig:Key"]));
                var tokenValidityMins = _configuaration.GetValue<int>("JwtConfig:TokenValidityMins");
                var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuaration["JwtConfig:Issuer"],
                    _configuaration["JwtConfig:Audience"],
                    claims,
                    expires: tokenExpiryTimeStamp,
                    signingCredentials: signIn
                    );

                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return new LoginResponseModel
                {
                    AccessToken = tokenValue,
                    UserName = req.UserName,
                    ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
                };

            }
            return null;

        }
    }
}
