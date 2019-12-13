using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            ProductoPedido = new HashSet<ProductoPedido>();
            Valoracion = new HashSet<Valoracion>();
        }

        public int Idpedido { get; set; }
        public int Idreserva { get; set; }
        public int? Cantidad { get; set; }
        public string Notas { get; set; }

        public virtual Reserva IdreservaNavigation { get; set; }
        public virtual ICollection<ProductoPedido> ProductoPedido { get; set; }
        public virtual ICollection<Valoracion> Valoracion { get; set; }
    }
}
