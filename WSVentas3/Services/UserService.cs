using WSVentas3.Models.Request;
using WSVentas3.Models.Response;
using WSVentas3.Models;
using WSVentas3.Tools;
using WSVentas3.Models.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WSVentas3.Services
{
    public class UserService :IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings) 
        {
            _appSettings = appSettings.Value;
        }

        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userResponse = new UserResponse();
            using (var db = new SistemaVentas1roContext())
            {
                string sPassword = Encrypt.GetSHA256(model.Password);
                var usuario = db.Usuario.Where(d => d.Email == model.Email 
                && d.Password == sPassword ).FirstOrDefault();
                if(usuario == null) return null;
                userResponse.Email = usuario.Email;
                userResponse.Token = GetToken(usuario);
            }
            return userResponse;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                            new Claim(ClaimTypes.Email, usuario.Email)
                        }
                    ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = 
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesc);
            return tokenHandler.WriteToken(token);
        } 
    }
}
