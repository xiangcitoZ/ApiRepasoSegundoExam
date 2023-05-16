using ApiRepasoSegundoExam.Data;
using ApiRepasoSegundoExam.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRepasoSegundoExam.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;

        public RepositoryUsuarios(UsuariosContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> ExisteUsuario(string username, string password)
        {
            return await this.context.Usuarios
                .FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }

        private async Task<int> GetMaxUserAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync(x => x.IdUsuario) + 1;
            }
        }

        public async Task RegisterAsync(Usuario user)
        {
            Usuario newUser = new Usuario()
            {
                IdUsuario = await this.GetMaxUserAsync(),
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Nombre = user.Nombre,
                Apellidos = user.Apellidos,
                ImagenPerfil = user.ImagenPerfil
            };

            this.context.Add(newUser);
            await this.context.SaveChangesAsync();
        }


        //PARTE DE TIENDA

        private async Task<int> GetMaxPedidoAsync()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Pedidos.MaxAsync(x => x.IdPedido) + 1;
            }
        }

        public async Task<List<Libro>> GetProductosAsync()
        {
            return await this.context.Libros.ToListAsync();
        }

        public async Task<Libro> FindProductoAsync(int id)
        {
            return await this.context.Libros.FirstOrDefaultAsync(x => x.IdLibro == id);
        }

        public async Task<List<VistaPedido>> GetPedidos(int idUser)
        {
            return await this.context.Vistapedidos.Where(x => x.IdUsuario == idUser).ToListAsync();
        }

        public async Task RegistrarPedidoAsync(Pedido pedido)
        {
            Pedido newPedido = new Pedido()
            {
                IdPedido = await this.GetMaxPedidoAsync(),
                IdFactura = 1,
                Fecha = DateTime.Now,
                IdLibro = pedido.IdLibro,
                IdUsuario = pedido.IdUsuario,
                Cantidad = pedido.Cantidad
            };

            this.context.Add(newPedido);
            await this.context.SaveChangesAsync();
        }
    }
}
