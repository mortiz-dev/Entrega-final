using Microsoft.AspNetCore.Mvc;
using Pre_Entrega_1.Handler;
using Pre_Entrega_1.Model;

namespace Entrega_final.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetAllProductos()
        {
            return ProductoHandler.GetProductos();
        }
    }
}
