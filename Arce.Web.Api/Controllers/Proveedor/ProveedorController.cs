using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Arce.Web.Service;
using Arce.Web.Entity.Proveedor;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        public readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }
        
        [HttpGet]
        [Route("getListarProveedorActivo")]
        public async Task<IActionResult> ListarProveedorActivo(int? Prv_Id, string? Prv_Nom, string? Prv_Ruc, string? Prv_Nom_Con, string? Flg_Est)
        {
            var result = await _proveedorService.ListarProveedorActivo(Prv_Id ?? 0, Prv_Nom ?? "", Prv_Ruc ?? "", Prv_Nom_Con ?? "", Flg_Est ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarProveedor")]
        public async Task<IActionResult> RegistrarProveedor([FromBody] ProveedorEntity valores)
        {
            ProveedorEntity parametros = new ProveedorEntity
            {
                Prv_Nom = valores.Prv_Nom,
                Prv_Ruc = valores.Prv_Ruc,
                Prv_Tel = valores.Prv_Tel,
                Prv_Dir = valores.Prv_Dir,
                Prv_Nom_Con = valores.Prv_Nom_Con,
                Usr_Reg = valores.Usr_Reg
            };
            
            var result = await _proveedorService.RegistrarProveedor(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarProveedor")]
        public async Task<IActionResult> ActualizarProveedor([FromBody] ProveedorEntity valores)
        {
            ProveedorEntity parametros = new ProveedorEntity
            {
                Prv_Id = valores.Prv_Id,
                Prv_Nom = valores.Prv_Nom,
                Prv_Ruc = valores.Prv_Tel,
                Prv_Tel = valores.Prv_Dir,
                Prv_Nom_Con = valores.Prv_Nom_Con,
                Flg_Est = valores.Flg_Est,
                Usr_Mod = valores.Usr_Mod
            };
            
            var result = await _proveedorService.RegistrarProveedor(parametros);
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
