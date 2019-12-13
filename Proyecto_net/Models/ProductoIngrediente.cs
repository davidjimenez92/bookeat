using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class ProductoIngrediente
    {
        public int Idingrediente { get; set; }
        public int Idproducto { get; set; }

        public virtual Ingrediente IdingredienteNavigation { get; set; }
        public virtual Producto IdproductoNavigation { get; set; }
    }
}
