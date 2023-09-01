using WSVentas3.Models.Request;
using WSVentas3.Models.Response;

namespace WSVentas3.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
