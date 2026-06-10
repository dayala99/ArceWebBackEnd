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
        public async Task<IActionResult> ListarUsuarioActivo(int? Usr_Id, string? Usr_Cod, string? Usr_Nom, string? Flg_Est)
        {
            var result = await _usuarioService.ListarUsuarioActivo(Usr_Id ?? 0, Usr_Cod ?? "", Usr_Nom ?? "", Flg_Est ?? "");
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
            var result = await _usuarioService.RegistrarUsuario(valores);
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
            var result = await _usuarioService.ActualizarUsuario(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerAccesoUsuario")]
        public async Task<IActionResult> ObtenerAccesoUsuario(string? Usr_Cod, string? Usr_Pass)
        {
            var result = await _usuarioService.ObtenerAccesoUsuario(Usr_Cod ?? "", Usr_Pass ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerUsuariosAprobacion")]
        public async Task<IActionResult> ObtenerUsuariosAprobacion(string? Usr_Apr)
        {
            var result = await _usuarioService.ObtenerUsuariosAprobacion(Usr_Apr ?? "");
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
