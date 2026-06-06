using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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
        public async Task<IActionResult> ListarPedido(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com)
        {
            var result = await _service.ListarPedido(Ped_Id ?? 0, Flg_Est ?? "", Ped_Tip_Com ?? 0);
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

        // [HttpPost]
        // [Route("postRegistrarPedido")]
        // public async Task<IActionResult> RegistrarPedido([FromBody] PedidoCabeceraEntity valores)
        // {
        //     if (valores == null)
        //     {
        //         return BadRequest(new
        //         {
        //             Success = false,
        //             CodeResult = StatusCodes.Status400BadRequest,
        //             Message = "El cuerpo de la solicitud de pedido es obligatorio."
        //         });
        //     }

        //     PedidoCabeceraEntity parametros = new PedidoCabeceraEntity
        //     {
        //         Ped_Id = valores.Ped_Id,
        //         Ped_Usr_Apr = valores.Ped_Usr_Apr,
        //         Ped_Lug_Ent = valores.Ped_Lug_Ent,
        //         Ped_Ref = valores.Ped_Ref,
        //         Ped_Tip_Com = valores.Ped_Tip_Com,
        //         Ped_Tip_Mon = valores.Ped_Tip_Mon,
        //         Ped_Fec_Ent = valores.Ped_Fec_Ent,
        //         Ped_Sus = valores.Ped_Sus,
        //         Ped_Arc_Adj_Nom = valores.Ped_Arc_Adj_Nom,
        //         Ped_Arc_Adj_Rut = valores.Ped_Arc_Adj_Rut,
        //         Ped_Prv_Cod = valores.Ped_Prv_Cod,
        //         Ped_For_Pag_Cod = valores.Ped_For_Pag_Cod,
        //         Usr_Reg = valores.Usr_Reg,
        //         Ped_Can_Tot = valores.Ped_Can_Tot
        //     };
            
        //     var result = await _service.RegistrarPedido(parametros);
        //     if (result!.Success)
        //     {
        //         result.CodeResult = StatusCodes.Status200OK;
        //         return Ok(result);
        //     }

        //     result.CodeResult = StatusCodes.Status400BadRequest;
        //     return BadRequest(result);
        // }

        [HttpPost]
        [Route("postRegistrarPedido")]
        public async Task<IActionResult> RegistrarPedido([FromForm] PedidoCabeceraEntity valores, IFormFile archivo)
        {
            if (valores == null)
            {
                return BadRequest(new { Success = false, Message = "Datos incompletos" });
            }

            if (archivo != null && archivo.Length > 0)
            {
                var carpeta = Path.Combine(@"C:\Archivos");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreArchivo = $"{Path.GetFileName(archivo.FileName)}";
                var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                valores.Ped_Arc_Adj_Nom = archivo.FileName;
                valores.Ped_Arc_Adj_Rut = rutaCompleta;
            }

            var result = await _service.RegistrarPedido(valores);
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
        public async Task<IActionResult> ActualizarPedido([FromForm] PedidoCabeceraEntity valores, IFormFile archivo)
        {
            if (valores == null)
            {
                return BadRequest(new { Success = false, Message = "Datos incompletos" });
            }

            if (archivo != null && archivo.Length > 0)
            {
                var carpeta = Path.Combine(@"C:\Archivos");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreArchivo = $"{Path.GetFileName(archivo.FileName)}";
                var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    await archivo.CopyToAsync(stream);
                }

                valores.Ped_Arc_Adj_Nom = archivo.FileName;
                valores.Ped_Arc_Adj_Rut = rutaCompleta;
            }

            // PedidoCabeceraEntity parametros = new PedidoCabeceraEntity
            // {
            //     Ped_Id = valores.Ped_Id,
            //     Ped_Usr_Apr = valores.Ped_Usr_Apr,
            //     Ped_Lug_Ent = valores.Ped_Lug_Ent,
            //     Ped_Ref = valores.Ped_Ref,
            //     Ped_Tip_Com = valores.Ped_Tip_Com,
            //     Ped_Tip_Mon = valores.Ped_Tip_Mon,
            //     Ped_Fec_Ent = valores.Ped_Fec_Ent,
            //     Ped_Sus = valores.Ped_Sus,
            //     Ped_Arc_Adj_Nom = valores.Ped_Arc_Adj_Nom,
            //     Ped_Arc_Adj_Rut = valores.Ped_Arc_Adj_Rut,
            //     Ped_Prv_Cod = valores.Ped_Prv_Cod,
            //     Ped_For_Pag_Cod = valores.Ped_For_Pag_Cod,
            //     Usr_Mod = valores.Usr_Mod,
            //     Ped_Can_Tot = valores.Ped_Can_Tot
            // };
            
            var result = await _service.ActualizarPedido(valores);
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
            // PedidoDetalleEntity parametros = new PedidoDetalleEntity
            // {
            //     Ped_Cab_Id = valores.Ped_Cab_Id,
            //     Ped_Cod_Itm = valores.Ped_Cod_Itm,
            //     Ped_Uni_Med = valores.Ped_Uni_Med,
            //     Ped_Can = valores.Ped_Can,
            //     Ped_Cos_Uni = valores.Ped_Cos_Uni,
            //     Ped_Cos_Tot = valores.Ped_Cos_Tot,
            //     Usr_Reg = valores.Usr_Reg
            // };
            
            var result = await _service.RegistrarDetallePedido(valores);
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
            
            var result = await _service.ActualizarDetallePedido(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("patchActualizarDetallePedido")]
        public async Task<IActionResult> EliminarDetallePedido([FromBody] PedidoDetalleEntity valores)
        {
            PedidoDetalleEntity parametros = new PedidoDetalleEntity
            {
                Ped_Det_Id = valores.Ped_Det_Id,
            };
            
            var result = await _service.EliminarDetallePedido(parametros);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchAsignarOrdenCompra")]
        public async Task<IActionResult> AsignarOrdenCompra([FromBody] PedidoCabeceraCentroCostoEntity valores)
        {            
            var result = await _service.AsignarOrdenCompra(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchAsignarOrdenCompraADetallePedido")]
        public async Task<IActionResult> AsignarOrdenCompraADetallePedido([FromBody] PedidoDetalleEntity valores)
        {            
            var result = await _service.AsignarOrdenCompraADetallePedido(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet("getArchivoPedido")]
        public IActionResult GetArchivoPedido(string nombreArchivo)
        {
            var carpeta = @"C:\Archivos";
            var ruta = Path.Combine(carpeta, nombreArchivo);

            if (!System.IO.File.Exists(ruta))
                return NotFound("El archivo no existe en disco");

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(ruta, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            var fileBytes = System.IO.File.ReadAllBytes(ruta);
            return File(fileBytes, mimeType);
        }

        [HttpGet]
        [Route("getListarItemsAsignadosPedidoCentroCosto")]
        public async Task<IActionResult> ListarItemsAsignadosPedidoCentroCosto(int Ped_Cab_Id)
        {
            var result = await _service.ListarItemsAsignadosPedidoCentroCosto(Ped_Cab_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarItemsAsignadosPedidoCentroCostoModificar")]
        public async Task<IActionResult> ListarItemsAsignadosPedidoCentroCostoModificar(int Ord_Com_Id, int Ped_Cab_Id)
        {
            var result = await _service.ListarItemsAsignadosPedidoCentroCostoModificar(Ord_Com_Id, Ped_Cab_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getCargarReportePedido")]
        public async Task<IActionResult> CargarReportePedido(string Ped_Id)
        {
            var result = await _service.CargarReportePedido(Ped_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchDesAsignarOrdenCompraADetallePedido")]
        public async Task<IActionResult> DesAsignarOrdenCompraADetallePedido([FromBody] PedidoDetalleEntity valores)
        {            
            var result = await _service.DesAsignarOrdenCompraADetallePedido(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarPedidoAprobadoParaOC")]
        public async Task<IActionResult> ListarPedidoAprobadoParaOC(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com)
        {
            var result = await _service.ListarPedidoAprobadoParaOC(Ped_Id ?? 0, Flg_Est ?? "", Ped_Tip_Com ?? 0);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarPedidoCuandoDetalleCompleto")]
        public async Task<IActionResult> ActualizarPedidoCuandoDetalleCompleto([FromBody] PedidoCabeceraEntity valores)
        {            
            var result = await _service.ActualizarPedidoCuandoDetalleCompleto(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarDetalleIngresoAlmacen")]
        public async Task<IActionResult> ListarDetalleIngresoAlmacen(int? Ped_Cab_Id, int? Ord_Com_Id)
        {
            var result = await _service.ListarDetalleIngresoAlmacen(Ped_Cab_Id ?? 0, Ord_Com_Id ?? 0);
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
