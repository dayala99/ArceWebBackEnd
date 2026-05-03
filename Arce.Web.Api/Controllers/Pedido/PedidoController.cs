using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        
        public readonly IPedidoService _service;

        public PedidoController(IPedidoService pedidoService)
        {
            _service = pedidoService;
        }

        [HttpGet]
        [Route("getListarPedido")]
        public async Task<IActionResult> ListarPedido(int? Ped_Id, string? Prv_Nom, string? Flg_Est, string? Ped_Tip_Com)
        {
            var result = await _service.ListarPedido(Ped_Id ?? 0, Prv_Nom ?? "", Flg_Est ?? "", Ped_Tip_Com ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarPedidoCorrelativoNuevo")]
        public async Task<IActionResult> ListarPedidoCorrelativoNuevo()
        {
            var result = await _service.ListarPedidoCorrelativoNuevo();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarPedidoModificar")]
        public async Task<IActionResult> ListarPedidoModificar(int Ped_Id)
        {
            var result = await _service.ListarPedidoModificar(Ped_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarPedido")]
        public async Task<IActionResult> RegistrarPedido([FromBody] PedidoCabeceraEntity valores)
        {
            PedidoCabeceraEntity parametros = new PedidoCabeceraEntity
            {
                Ped_Id = valores.Ped_Id,
                Ped_Usr_Apr = valores.Ped_Usr_Apr,
                Ped_Lug_Ent = valores.Ped_Lug_Ent,
                Ped_Ref = valores.Ped_Ref,
                Ped_Tip_Com = valores.Ped_Tip_Com,
                Ped_Tip_Mon = valores.Ped_Tip_Mon,
                Ped_Fec_Ent = valores.Ped_Fec_Ent,
                Ped_Sus = valores.Ped_Sus,
                Ped_Arc_Adj_Nom = valores.Ped_Arc_Adj_Nom,
                Ped_Arc_Adj_Rut = valores.Ped_Arc_Adj_Rut,
                Ped_Prv_Cod = valores.Ped_Prv_Cod,
                Ped_For_Pag_Cod = valores.Ped_For_Pag_Cod,
                Usr_Reg = valores.Usr_Reg,
                Ped_Can_Tot = valores.Ped_Can_Tot
            };
            
            var result = await _service.RegistrarPedido(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarPedido")]
        public async Task<IActionResult> ActualizarPedido([FromBody] PedidoCabeceraEntity valores)
        {
            PedidoCabeceraEntity parametros = new PedidoCabeceraEntity
            {
                Ped_Id = valores.Ped_Id,
                Ped_Usr_Apr = valores.Ped_Usr_Apr,
                Ped_Lug_Ent = valores.Ped_Lug_Ent,
                Ped_Ref = valores.Ped_Ref,
                Ped_Tip_Com = valores.Ped_Tip_Com,
                Ped_Tip_Mon = valores.Ped_Tip_Mon,
                Ped_Fec_Ent = valores.Ped_Fec_Ent,
                Ped_Sus = valores.Ped_Sus,
                Ped_Arc_Adj_Nom = valores.Ped_Arc_Adj_Nom,
                Ped_Arc_Adj_Rut = valores.Ped_Arc_Adj_Rut,
                Ped_Prv_Cod = valores.Ped_Prv_Cod,
                Ped_For_Pag_Cod = valores.Ped_For_Pag_Cod,
                Usr_Mod = valores.Usr_Mod,
                Ped_Can_Tot = valores.Ped_Can_Tot
            };
            
            var result = await _service.ActualizarPedido(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarPedidoEstado")]
        public async Task<IActionResult> ActualizarPedidoEstado([FromBody] PedidoCabeceraEntity valores)
        {
            PedidoCabeceraEntity parametros = new PedidoCabeceraEntity
            {
                Ped_Id = valores.Ped_Id,
                Flg_Est = valores.Flg_Est
            };
            
            var result = await _service.ActualizarPedidoEstado(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarPedidoRegistradoCentroCosto")]
        public async Task<IActionResult> ListarPedidoRegistradoCentroCosto(int? Ped_Id)
        {
            var result = await _service.ListarPedidoRegistradoCentroCosto(Ped_Id ?? 0);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarPedidoRegistradoCentroCostoModificar")]
        public async Task<IActionResult> ListarPedidoRegistradoCentroCostoModificar(int? Ped_Cen_Cos_Id)
        {
            var result = await _service.ListarPedidoRegistradoCentroCostoModificar(Ped_Cen_Cos_Id ?? 0);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarCentroCostoPedidoRegistrado")]
        public async Task<IActionResult> RegistrarCentroCostoPedidoRegistrado([FromBody] PedidoCabeceraCentroCostoEntity valores)
        {
            PedidoCabeceraCentroCostoEntity parametros = new PedidoCabeceraCentroCostoEntity
            {
                Ped_Id = valores.Ped_Id,
                Ped_Cen_Cos = valores.Ped_Cen_Cos,
                Ped_Can = valores.Ped_Can
            };
            
            var result = await _service.RegistrarCentroCostoPedidoRegistrado(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarCentroCostoPedidoRegistrado")]
        public async Task<IActionResult> EliminarCentroCostoPedidoRegistrado([FromBody] PedidoCabeceraCentroCostoEntity valores)
        {
            PedidoCabeceraCentroCostoEntity parametros = new PedidoCabeceraCentroCostoEntity
            {
                Ped_Cen_Cos_Id = valores.Ped_Cen_Cos_Id
            };
            
            var result = await _service.EliminarCentroCostoPedidoRegistrado(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getObtenerTotalPedidoPorCenCos")]
        public async Task<IActionResult> ObtenerTotalPedidoPorCenCos(int Ped_Id, string Ped_Cen_Cos)
        {
            var result = await _service.ObtenerTotalPedidoPorCenCos(Ped_Id, Ped_Cen_Cos);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDetallePedido")]
        public async Task<IActionResult> ListarDetallePedido(int Ped_Cab_Id)
        {
            var result = await _service.ListarDetallePedido(Ped_Cab_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDetallePedidoModificar")]
        public async Task<IActionResult> ListarDetallePedidoModificar(int Ped_Det_Id)
        {
            var result = await _service.ListarDetallePedidoModificar(Ped_Det_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarDetallePedido")]
        public async Task<IActionResult> RegistrarDetallePedido([FromBody] PedidoDetalleEntity valores)
        {
            PedidoDetalleEntity parametros = new PedidoDetalleEntity
            {
                Ped_Cab_Id = valores.Ped_Cab_Id,
                Ped_Cod_Itm = valores.Ped_Cod_Itm,
                Ped_Uni_Med = valores.Ped_Uni_Med,
                Ped_Can = valores.Ped_Can,
                Ped_Cos_Uni = valores.Ped_Cos_Uni,
                Ped_Cos_Tot = valores.Ped_Cos_Tot,
                Usr_Reg = valores.Usr_Reg
            };
            
            var result = await _service.RegistrarDetallePedido(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarDetallePedido")]
        public async Task<IActionResult> ActualizarDetallePedido([FromBody] PedidoDetalleEntity valores)
        {
            PedidoDetalleEntity parametros = new PedidoDetalleEntity
            {
                Ped_Det_Id = valores.Ped_Det_Id,
                Ped_Cod_Itm = valores.Ped_Cod_Itm,
                Ped_Uni_Med = valores.Ped_Uni_Med,
                Ped_Can = valores.Ped_Can,
                Ped_Cos_Uni = valores.Ped_Cos_Uni,
                Ped_Cos_Tot = valores.Ped_Cos_Tot,
                Usr_Mod = valores.Usr_Mod
            };
            
            var result = await _service.RegistrarDetallePedido(parametros);
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
