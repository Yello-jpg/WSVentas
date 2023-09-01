using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVentas.Models.Request;
using WSVentas.Models.Response;
using WSVentas3.Models;

namespace WSVentas3.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta res = new Respuesta();
            try
            {
                using (SistemaVentas1roContext db = new SistemaVentas1roContext())
                {
                    var lstC = db.Cliente.OrderByDescending(d => d.Id).ToList();
                    foreach (var cli in lstC)
                    {
                        cli.Venta = db.Venta.Where(v => v.IdCliente == cli.Id).ToList();
                    }
                    res.Data = lstC;
                    res.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
            }
            return Ok(res);
        }


        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SistemaVentas1roContext db = new SistemaVentas1roContext())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.Name = oModel.name;
                    db.Cliente.Add(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SistemaVentas1roContext db = new SistemaVentas1roContext())
                {
                    Cliente oCliente = db.Cliente.Find(oModel.id);
                    oCliente.Name = oModel.name;
                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{id}")]
        public IActionResult Del(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (SistemaVentas1roContext db = new SistemaVentas1roContext())
                {
                    Cliente oCliente = db.Cliente.Find(id);
                    db.Remove(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
