using Entrega_final.Controllers.DOTS;
using Entrega_final.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Entrega_final.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentasController : ControllerBase
    {
        [HttpGet("ObtenerProductosVendidos")]
        public List<GetProductosVendidos> GetProductoVendido()
        {
            return VentasHandler.GetProductosVentas();
        }

        [HttpGet("ObtenerListaVentas")]
        public List<GetVentas> GetListaVentas()
        {
            return VentasHandler.GetListVentas();
        }

        [HttpDelete]
        public void EliminarVenta([FromBody] int id)
        {
            VentasHandler.DeleteVenta(id);
        }
    }
}
