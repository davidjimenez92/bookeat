using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class Reserva
    {
        public Reserva()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int Idreserva { get; set; }
        public int Idusuario { get; set; }
        public int Idlocal { get; set; }
        public int? Idmesa { get; set; }
        public DateTime? Fecha { get; set; }
        public string Notas { get; set; }

        public virtual Local IdlocalNavigation { get; set; }
        public virtual Mesa IdmesaNavigation { get; set; }
        public virtual Usuario IdusuarioNavigation { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
