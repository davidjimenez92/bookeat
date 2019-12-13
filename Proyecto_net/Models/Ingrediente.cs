using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class Ingrediente
    {
        public Ingrediente()
        {
            ProductoIngrediente = new HashSet<ProductoIngrediente>();
        }

        public int Idingrediente { get; set; }
        public string Nombre { get; set; }
        public bool? Alergeno { get; set; }

        public virtual ICollection<ProductoIngrediente> ProductoIngrediente { get; set; }
    }
}
