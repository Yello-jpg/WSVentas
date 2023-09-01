using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVentas.Models.Response;
using WSVentas3.Models.Request;
using WSVentas3.Services;

namespace WSVentas3.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            Respuesta res = new Respuesta();
            var userResponse = _userService.Auth(model);
            if (userResponse == null)
            {
                res.Exito = 0;
                res.Mensaje = "Correo o contraseña incorrecta.";
                return BadRequest(res);
            }
            res.Exito = 1;
            res.Data = userResponse;
            return Ok(res);
        }
    }
}
