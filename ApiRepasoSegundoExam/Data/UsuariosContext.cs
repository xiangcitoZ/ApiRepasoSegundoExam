using ApiRepasoSegundoExam.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRepasoSegundoExam.Data
{
    public class UsuariosContext : DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) 
            : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<VistaPedido> Vistapedidos { get; set; }
    }
}
