using ApiRepasoSegundoExam.Models;
using ApiRepasoSegundoExam.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiRepasoSegundoExam.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaController : ControllerBase
    {

        private RepositoryUsuarios repo;

        public TiendaController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Libro>>> Productos()
        {
            List<Libro> list = await this.repo.GetProductosAsync();
            return list;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult<Libro>> Producto(int id)
        {
            Libro producto = await this.repo.FindProductoAsync(id);
            return producto;
        }

        [Authorize]
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<VistaPedido>>> Pedidos()
        {
            string jsonUser =
                HttpContext.User.Claims.SingleOrDefault(z => z.Type == "USERDATA").Value;
            Usuario user = JsonConvert.DeserializeObject<Usuario>(jsonUser);

            List<VistaPedido> pedidos = await this.repo.GetPedidos(user.IdUsuario);
            return pedidos;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> RealizarPedido(Pedido pedido)
        {
            await this.repo.RegistrarPedidoAsync(pedido);
            return Ok();
        }

    }
}
