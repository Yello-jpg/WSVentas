using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVentas.Models.Response;
using WSVentas3.Models;
using WSVentas3.Models.Request;
using WSVentas3.Services;

namespace WSVentas3.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private IVentaService _venta;

        public VentaController(IVentaService venta) {
            this._venta = venta;
        }

        [HttpPost]
        public IActionResult Add(VentaRequest model)
        {
            Respuesta res = new Respuesta();

            try
            {
                //var venta = new VentaService();
                _venta.Add(model);
                res.Exito = 1;
            }catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }
    }
}
