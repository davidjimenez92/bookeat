using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class Mesa
    {
        public Mesa()
        {
            Reserva = new HashSet<Reserva>();
        }

        public int Idmesa { get; set; }
        public int Idlocal { get; set; }
        public short? Capacidad { get; set; }
        public short? numeroMesa { get; set; }
        public string Notas { get; set; }

        public virtual Local IdlocalNavigation { get; set; }
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
