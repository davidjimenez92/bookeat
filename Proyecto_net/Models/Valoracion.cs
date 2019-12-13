using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class Valoracion
    {
        public int Idvaloracion { get; set; }
        public int Idpedido { get; set; }
        public int Valor { get; set; }
        public string Comentario { get; set; }

        public virtual Pedido IdpedidoNavigation { get; set; }
    }
}
