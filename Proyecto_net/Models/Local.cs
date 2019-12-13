using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_net.Models
{
    public partial class Local
    {
        public Local()
        {
            Imagen = new HashSet<Imagen>();
            Mesa = new HashSet<Mesa>();
            Producto = new HashSet<Producto>();
            Reserva = new HashSet<Reserva>();
        }

        public int Idlocal { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Pw { get; set; }
        public string Horario { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
        public string Barrio { get; set; }

        public string CPostal { get; set; }
        public string Coordenadas { get; set; }
        public string Telefono { get; set; }
        public string Notas { get; set; }
        public string Categoria { get; set; }

        public virtual ICollection<Imagen> Imagen { get; set; }
        public virtual ICollection<Mesa> Mesa { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
