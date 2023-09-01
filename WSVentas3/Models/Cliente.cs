using System;
using System.Collections.Generic;

namespace WSVentas3.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
