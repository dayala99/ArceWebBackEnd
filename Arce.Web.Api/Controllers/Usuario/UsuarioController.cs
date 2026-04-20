using Microsoft.AspNetCore.Http;
using Arce.Web.Service;
using Arce.Web.Entity.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpGet]
        [Route("getListarUsuarioActivo")]
        public async Task<IActionResult> ListarUsuarioActivo(string Usr_Id, string Usr_Cod, string Usr_Nom, string Flg_Est)
        {
            var result = await _usuarioService.ListarUsuarioActivo(Usr_Id, Usr_Cod, Usr_Nom, Flg_Est);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarUsuario")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioEntity valores)
        {
            UsuarioEntity parametros = new UsuarioEntity
            {
                Usr_Cod = valores.Usr_Cod,
                Usr_Nom = valores.Usr_Nom,
                Flg_Est = valores.Flg_Est,
                Usr_Reg = valores.Usr_Reg
            };
            
            var result = await _usuarioService.RegistrarUsuario(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] UsuarioEntity valores)
        {
            UsuarioEntity parametros = new UsuarioEntity
            {
                Usr_Id = valores.Usr_Id,
                Usr_Cod = valores.Usr_Cod,
                Usr_Nom = valores.Usr_Nom,
                Flg_Est = valores.Flg_Est,
                Usr_Reg = valores.Usr_Reg
            };
            
            var result = await _usuarioService.ActualizarUsuario(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
    }
}
