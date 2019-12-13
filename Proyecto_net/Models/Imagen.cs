using System;
using System.Collections.Generic;

namespace Proyecto_net.Models
{
    public partial class Imagen
    {
        public int Idimagen { get; set; }
        public int Idlocal { get; set; }
        public string Url { get; set; }

        public virtual Local IdlocalNavigation { get; set; }
    }
}
