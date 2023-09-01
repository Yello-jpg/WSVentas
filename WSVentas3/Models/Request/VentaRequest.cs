using System.ComponentModel.DataAnnotations;

namespace WSVentas3.Models.Request
{
    public class VentaRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El valor IdCliente debe ser valido")]
        [ExisteIdCliente(ErrorMessage = "El idCliente no existe")]
        public int IdCliente { get; set; }
        [Required]
        [MinLength(1,ErrorMessage = "Deben existir conceptos")]
        public List<Concepto> Conceptos { get; set; }

        public VentaRequest()
        {
            this.Conceptos = new List<Concepto>();
        }
    }
    public class Concepto
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdProducto { get; set; }
    }
    public class ExisteIdCliente : ValidationAttribute {
        public override bool IsValid(object value)
        {
            int idCliente = (int)value;
            using (SistemaVentas1roContext db = new SistemaVentas1roContext())
            {
                if (db.Cliente.Find(idCliente) == null) return false;
            }
            return true;
        }
    }
}
