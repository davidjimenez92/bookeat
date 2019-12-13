using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class ProductoPedido
    {
        public int Idproducto { get; set; }
        public int Idpedido { get; set; }

        public virtual Pedido IdpedidoNavigation { get; set; }
        public virtual Producto IdproductoNavigation { get; set; }
    }
}
