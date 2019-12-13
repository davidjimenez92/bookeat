using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class Producto
    {
        public Producto()
        {
            ProductoIngrediente = new HashSet<ProductoIngrediente>();
            ProductoPedido = new HashSet<ProductoPedido>();
        }

        public int Idproducto { get; set; }
        public int Idlocal { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public double? Precio { get; set; }
        public string Notas { get; set; }

        public virtual Local IdlocalNavigation { get; set; }
        public virtual ICollection<ProductoIngrediente> ProductoIngrediente { get; set; }
        public virtual ICollection<ProductoPedido> ProductoPedido { get; set; }
    }
}
