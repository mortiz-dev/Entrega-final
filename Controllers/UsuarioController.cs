using Microsoft.AspNetCore.Mvc;
using Entrega_final.Controllers.DTOS;
using MiPrimeraApi2.Repository;
using Entrega_final.Model;
using Entrega_final.Controllers.DOTS;
using Entrega_final.Handler.DOTS;

namespace MiPrimeraApi2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpDelete]
        public void EliminarUsuario([FromBody] int id)
        {
            try
            {
                UsuarioHandler.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuarioEdit usuario)
        {
            return UsuarioHandler.ModificarNombreDeUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre
            });
        }

        [HttpPost]
        public void CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                UsuarioHandler.CrearUsuario(new Usuario
                {
                    Apellido = usuario.Apellido,
                    Password = usuario.Contraseña,
                    Email = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public GetUserName GetUsuariosById(int id)
        {
            return UsuarioHandler.GetNombreUsuario(id);
        }
        [HttpPost("IniciarSesion")]
        public bool IniciarSesion([FromBody] UserLogin login)
        {
            return UsuarioHandler.UserLogin(new UserLogin { 
                UserName = login.UserName,
                Password = login.Password 
            });
        }
    }
}
