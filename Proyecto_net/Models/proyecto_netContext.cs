using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Proyecto_net.Models
{
    public partial class proyecto_netContext : DbContext
    {
        public proyecto_netContext()
        {
        }

        public proyecto_netContext(DbContextOptions<proyecto_netContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Imagen> Imagen { get; set; }
        public virtual DbSet<Ingrediente> Ingrediente { get; set; }
        public virtual DbSet<Local> Local { get; set; }
        public virtual DbSet<Mesa> Mesa { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoIngrediente> ProductoIngrediente { get; set; }
        public virtual DbSet<ProductoPedido> ProductoPedido { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Valoracion> Valoracion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=proyecto_net;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imagen>(entity =>
            {
                entity.HasKey(e => e.Idimagen)
                    .HasName("PK__Imagen__462D39205C5A211E");

                entity.Property(e => e.Idimagen).HasColumnName("IDImagen");

                entity.Property(e => e.Idlocal).HasColumnName("IDLocal");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdlocalNavigation)
                    .WithMany(p => p.Imagen)
                    .HasForeignKey(d => d.Idlocal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Imagen__IDLocal__412EB0B6");
            });

            modelBuilder.Entity<Ingrediente>(entity =>
            {
                entity.HasKey(e => e.Idingrediente)
                    .HasName("PK__Ingredie__2E73012FDC063FF4");

                entity.Property(e => e.Idingrediente).HasColumnName("IDIngrediente");

                entity.Property(e => e.Alergeno).HasColumnName("alergeno");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(20)
                    .IsUnicode(false);
                
            });

            modelBuilder.Entity<Local>(entity =>
            {
                entity.HasKey(e => e.Idlocal)
                    .HasName("PK__Local__E694E680A62E91C7");

                entity.Property(e => e.Idlocal).HasColumnName("IDLocal");

                entity.Property(e => e.CPostal)
                    .HasColumnName("cPostal")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Ciudad)
                    .HasColumnName("ciudad")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Distrito)
                    .HasColumnName("distrito")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Barrio)
                    .HasColumnName("barrio")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Coordenadas)
                    .HasColumnName("coordenadas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasColumnName("direccion")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Horario)
                    .HasColumnName("horario")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Notas)
                    .HasColumnName("notas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pw)
                    .HasColumnName("pw")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Categoria)
                    .HasColumnName("categoria")
                    .HasMaxLength(300);

                entity.Property(e => e.Distrito)
                    .HasColumnName("distrito")
                    .HasMaxLength(300);

                entity.Property(e => e.Barrio)
                    .HasColumnName("barrio")
                    .HasMaxLength(300);
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.HasKey(e => e.Idmesa)
                    .HasName("PK__Mesa__089D17AA90E676C2");

                entity.Property(e => e.Idmesa).HasColumnName("IDMesa");

                entity.Property(e => e.Capacidad).HasColumnName("capacidad");

                entity.Property(e => e.numeroMesa).HasColumnName("numeroMesa");

                entity.Property(e => e.Idlocal).HasColumnName("IDLocal");

                entity.Property(e => e.Notas)
                    .HasColumnName("notas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdlocalNavigation)
                    .WithMany(p => p.Mesa)
                    .HasForeignKey(d => d.Idlocal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Mesa__IDLocal__37A5467C");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Idpedido)
                    .HasName("PK__Pedido__00C11F997B779ACF");

                entity.Property(e => e.Idpedido).HasColumnName("IDPedido");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Idreserva).HasColumnName("IDReserva");

                entity.Property(e => e.Notas)
                    .HasColumnName("notas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdreservaNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.Idreserva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pedido__IDReserv__3F466844");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproducto)
                    .HasName("PK__Producto__ABDAF2B44CF2EC6C");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.Property(e => e.Categoria)
                    .HasColumnName("categoria")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idlocal).HasColumnName("IDLocal");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Notas)
                    .HasColumnName("notas")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.HasOne(d => d.IdlocalNavigation)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.Idlocal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__IDLoca__38996AB5");
            });

            modelBuilder.Entity<ProductoIngrediente>(entity =>
            {
                entity.HasKey(e => new { e.Idingrediente, e.Idproducto })
                    .HasName("PK__Producto__74CEAE040E06F40D");

                entity.Property(e => e.Idingrediente).HasColumnName("IDIngrediente");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.HasOne(d => d.IdingredienteNavigation)
                    .WithMany(p => p.ProductoIngrediente)
                    .HasForeignKey(d => d.Idingrediente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductoI__IDIng__3A81B327");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.ProductoIngrediente)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductoI__IDPro__3B75D760");
            });

            modelBuilder.Entity<ProductoPedido>(entity =>
            {
                entity.HasKey(e => new { e.Idpedido, e.Idproducto })
                    .HasName("PK__Producto__5A7CB0B23122F8A3");

                entity.Property(e => e.Idpedido).HasColumnName("IDPedido");

                entity.Property(e => e.Idproducto).HasColumnName("IDProducto");

                entity.HasOne(d => d.IdpedidoNavigation)
                    .WithMany(p => p.ProductoPedido)
                    .HasForeignKey(d => d.Idpedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductoP__IDPed__3E52440B");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.ProductoPedido)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductoP__IDPro__3D5E1FD2");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.Idreserva)
                    .HasName("PK__Reserva__D9F2FA6768F7F55B");

                entity.Property(e => e.Idreserva).HasColumnName("IDReserva");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Idlocal).HasColumnName("IDLocal");

                entity.Property(e => e.Idmesa).HasColumnName("IDMesa");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Notas)
                    .HasColumnName("notas")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdlocalNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.Idlocal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reserva__IDLocal__35BCFE0A");

                entity.HasOne(d => d.IdmesaNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.Idmesa)
                    .HasConstraintName("FK__Reserva__IDMesa__36B12243");

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reserva__IDUsuar__34C8D9D1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__Usuario__523111699B86D053");

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Apellido)
                    .HasColumnName("apellido")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasColumnName("correo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fechaNacimiento")
                    .HasColumnType("date");

                entity.Property(e => e.FotoPerfil)
                    .HasColumnName("fotoPerfil")
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Pw)
                    .HasColumnName("pw")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasColumnName("telefono")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Valoracion>(entity =>
            {
                entity.HasKey(e => e.Idvaloracion)
                    .HasName("PK__Valoraci__FAE33E754EAE00E5");

                entity.Property(e => e.Idvaloracion).HasColumnName("IDValoracion");

                entity.Property(e => e.Comentario)
                    .HasColumnName("comentario")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Idpedido).HasColumnName("IDPedido");

                entity.HasOne(d => d.IdpedidoNavigation)
                    .WithMany(p => p.Valoracion)
                    .HasForeignKey(d => d.Idpedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Valoracio__IDPed__403A8C7D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
