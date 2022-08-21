using Entrega_final.Handler.DOTS;
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
        [HttpPost]
        public void CrearProducto([FromBody] PutProducto producto)
        {
            try
            {
                ProductoHandler.CrearProducto(new Producto
                {
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [HttpDelete]
        public void DeleteProducto([FromBody] int id)
        {
            ProductoHandler.DeleteProducto(id);
        }
    }
}
