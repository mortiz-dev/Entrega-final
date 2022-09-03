using Entrega_final.Handler.DOTS;
using Microsoft.AspNetCore.Mvc;
using Pre_Entrega_1.Handler;

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
    }
}
