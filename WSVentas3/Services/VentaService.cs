using WSVentas3.Models;
using WSVentas3.Models.Request;

namespace WSVentas3.Services
{
    public class VentaService : IVentaService
    {
        public VentaService() { }
        public void Add(VentaRequest model){
            using (SistemaVentas1roContext db = new SistemaVentas1roContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var venta = new Venta();
                        venta.Total = model.Conceptos.Sum(d => d.Cantidad * db.Producto.Find(d.IdProducto).PrecioUnitario);
                        venta.Fecha = DateTime.Now;
                        venta.IdCliente = model.IdCliente;
                        db.Venta.Add(venta);
                        db.SaveChanges();

                        foreach (var modelConcepto in model.Conceptos)
                        {
                            var concepto = new Models.Concepto();
                            concepto.IdProducto = modelConcepto.IdProducto;
                            concepto.IdVenta = venta.Id;
                            concepto.Cantidad = modelConcepto.Cantidad;
                            concepto.PrecioUnitario = db.Producto.Find(concepto.IdProducto).PrecioUnitario;
                            concepto.Importe = concepto.PrecioUnitario * concepto.Cantidad;
                            db.Concepto.Add(concepto);
                            db.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Ocurrio un error en la inserción");
                    }
                }
            }
        }
    }
}
