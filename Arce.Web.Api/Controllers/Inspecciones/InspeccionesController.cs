using Arce.Web.Api.Models.Inspecciones;
using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.IO;

namespace Arce.Web.Api.Controllers.Inspecciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class InspeccionesController : ControllerBase
    {
        private readonly IInspeccionesService _inspeccionesService;

        public InspeccionesController(IInspeccionesService inspeccionesService)
        {
            _inspeccionesService = inspeccionesService;
        }

        [HttpGet]
        [Route("getConsultaDatosUsuario")]
        public async Task<IActionResult> ConsultarDatosUsuario(string? Usr_Cod)
        {
            var result = await _inspeccionesService.ConsultarDatosUsuario(Usr_Cod ?? "");
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getSubEstacionesPorCliente")]
        public async Task<IActionResult> ListarSubEstacionesPorCliente(int Cliente_Id)
        {
            var result = await _inspeccionesService.ListarSubEstacionesPorCliente(Cliente_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarWeReport")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> InsertarWeReport([FromForm] RegistrarWeReportFormRequest valores)
        {
            var carpeta = @"C:\Inspecciones\We_Report";
            Directory.CreateDirectory(carpeta);

            var foto1Ubicacion = await GuardarArchivosAsync(valores.Report_Foto1, carpeta, "Foto1");
            var foto2Ubicacion = await GuardarArchivosAsync(valores.Report_Foto2, carpeta, "Foto2");

            var entidad = new WeReportEntity
            {
                Usr_Cod = valores.Usr_Cod,
                Report_Anonimo = NormalizarMarca(valores.Report_Anonimo),
                Reporte_Id = valores.Reporte_Id,
                Cen_Cos_Id = valores.Cen_Cos_Id,
                Cliente_Id = valores.Cliente_Id,
                Subestacion_Id = valores.Subestacion_Id,
                Report_Descripcion = valores.Report_Descripcion,
                Report_Foto1_Ubicacion = foto1Ubicacion,
                Report_Acciones_Inmediata = valores.Report_Acciones_Inmediata,
                Report_Foto2_Ubicacion = foto2Ubicacion,
                Report_Acciones_Propuestas = valores.Report_Acciones_Propuestas,
                Report_Potencial = valores.Report_Potencial,
                Report_Aplica = NormalizarMarca(valores.Report_Aplica),
                Usr_Reg = valores.Usr_Reg,
                Fec_Reg = DateTime.Now,
                Estado = "A"
            };

            var result = await _inspeccionesService.InsertarWeReport(entidad);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }


        [HttpGet]
        [Route("getArchivoWeReport")]
        public IActionResult GetArchivoWeReport(string rutaArchivo)
        {
            if (string.IsNullOrWhiteSpace(rutaArchivo))
            {
                return BadRequest("La ruta del archivo es obligatoria");
            }

            var ruta = rutaArchivo.Trim();
            if (!Path.IsPathRooted(ruta))
            {
                ruta = Path.Combine(@"C:\Inspecciones\We_Report", ruta);
            }

            if (!System.IO.File.Exists(ruta))
            {
                return NotFound("El archivo no existe en disco");
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(ruta, out var mimeType))
            {
                mimeType = "application/octet-stream";
            }

            var fileBytes = System.IO.File.ReadAllBytes(ruta);
            return File(fileBytes, mimeType);
        }

        [HttpGet]
        [Route("getListarSubEstaciones")]
        public async Task<IActionResult> ListarSubEstaciones(int? Id, string? Nombre, int? Cliente_Id, string? Estado)
        {
            var result = await _inspeccionesService.ListarSubEstaciones(Id, Nombre, Cliente_Id, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        // NUEVO: listado simple (sin filtros) de subestaciones para el combo de We Report
        [HttpGet]
        [Route("getListarSubEstacionesReporte")]
        public async Task<IActionResult> ListarSubEstacionesReporte()
        {
            var result = await _inspeccionesService.ListarSubEstacionesReporte();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            var result = await _inspeccionesService.ListarClientes();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarMotivos")]
        public async Task<IActionResult> ListarMotivos()
        {
            var result = await _inspeccionesService.ListarMotivos();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarClimas")]
        public async Task<IActionResult> ListarClimas()
        {
            var result = await _inspeccionesService.ListarClimas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarTareas")]
        public async Task<IActionResult> ListarTareas()
        {
            var result = await _inspeccionesService.ListarTareas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarSubContratas")]
        public async Task<IActionResult> ListarSubContratas()
        {
            var result = await _inspeccionesService.ListarSubContratas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarJefesArea")]
        public async Task<IActionResult> ListarJefesArea()
        {
            var result = await _inspeccionesService.ListarJefesArea();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarTiposReporte")]
        public async Task<IActionResult> ListarTiposReporte()
        {
            var result = await _inspeccionesService.ListarTiposReporte();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getFiltrarWeReport")]
        public async Task<IActionResult> FiltrarWeReport(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
        {
            var result = await _inspeccionesService.FiltrarWeReport(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        // NUEVO: devuelve Cen_Cos_Des y DNI del jefe a partir de su Usr_Cod
        [HttpGet]
        [Route("getMostrarJefe")]
        public async Task<IActionResult> MostrarJefe(string Jefe_Cod)
        {
            var result = await _inspeccionesService.MostrarJefe(Jefe_Cod);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getListarObservacionesPlaneadas")]
        public async Task<IActionResult> ListarObservacionesPlaneadas()
        {
            var result = await _inspeccionesService.ListarObservacionesPlaneadas();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getConsultarEstadoObservaciones")]
        public async Task<IActionResult> ConsultarEstadoObservaciones(string Estado)
        {
            var result = await _inspeccionesService.ConsultarEstadoObservaciones(Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getFiltrarObservaciones")]
        public async Task<IActionResult> FiltrarObservaciones(DateTime Fecha_Desde, DateTime Fecha_Hasta, string Estado)
        {
            var result = await _inspeccionesService.FiltrarObservaciones(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMostrarObservacionPlaneada")]
        public async Task<IActionResult> MostrarObservacionPlaneada(string Codigo_Obs)
        {
            var result = await _inspeccionesService.MostrarObservacionPlaneada(Codigo_Obs);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postRegistrarObservacionPlaneada")]
        public async Task<IActionResult> RegistrarObservacionPlaneada([FromBody] ObservacionPlaneadaEntity valores)
        {
            var result = await _inspeccionesService.RegistrarObservacionPlaneada(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPatch]
        [Route("patchActualizarObservacionPlaneada")]
        public async Task<IActionResult> ActualizarObservacionPlaneada([FromBody] ActualizarObservacionPlaneadaEntity valores)
        {
            var result = await _inspeccionesService.ActualizarObservacionPlaneada(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postEliminarObservacionPlaneada")]
        public async Task<IActionResult> EliminarObservacionPlaneada([FromBody] EliminarObservacionPlaneadaEntity valores)
        {
            var result = await _inspeccionesService.EliminarObservacionPlaneada(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }
        [HttpGet]
        [Route("getListarTiposInspeccion")]
        public async Task<IActionResult> ListarTiposInspeccion()
        {
            var result = await _inspeccionesService.ListarTiposInspeccion();
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarMedioAmbiente")]
        public async Task<IActionResult> InsertarMedioAmbiente([FromBody] InsMedioAmbienteEntity valores)
        {
            var result = await _inspeccionesService.InsertarMedioAmbiente(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getFiltrarPrevencion")]
        public async Task<IActionResult> FiltrarPrevencion(DateTime Fecha_Desde, DateTime Fecha_Hasta, string Estado)
        {
            var result = await _inspeccionesService.FiltrarPrevencion(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMostrarPrevencion")]
        public async Task<IActionResult> MostrarPrevencion(int Prevencion_Id)
        {
            var result = await _inspeccionesService.MostrarPrevencion(Prevencion_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarPrevencion")]
        public async Task<IActionResult> InsertarPrevencion([FromBody] InsPrevencionEntity valores)
        {
            var result = await _inspeccionesService.InsertarPrevencion(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPut]
        [Route("putActualizarPrevencion")]
        public async Task<IActionResult> ActualizarPrevencion([FromBody] ActualizarPrevencionEntity valores)
        {
            var result = await _inspeccionesService.ActualizarPrevencion(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarPrevencion")]
        public async Task<IActionResult> EliminarPrevencion(int Prevencion_Id, string Usr_Mod)
        {
            var valores = new EliminarPrevencionEntity { Prevencion_Id = Prevencion_Id, Usr_Mod = Usr_Mod };
            var result = await _inspeccionesService.EliminarPrevencion(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getFiltrarMedioAmbiente")]
        public async Task<IActionResult> FiltrarMedioAmbiente(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
        {
            var result = await _inspeccionesService.FiltrarMedioAmbiente(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getMostrarMedioAmbiente")]
        public async Task<IActionResult> MostrarMedioAmbiente(int Medio_Ambiente_Id)
        {
            var result = await _inspeccionesService.MostrarMedioAmbiente(Medio_Ambiente_Id);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPut]
        [Route("putActualizarMedioAmbiente")]
        public async Task<IActionResult> ActualizarMedioAmbiente([FromBody] ActualizarMedioAmbienteEntity valores)
        {
            var result = await _inspeccionesService.ActualizarMedioAmbiente(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarMedioAmbiente")]
        public async Task<IActionResult> EliminarMedioAmbiente(int Medio_Ambiente_Id, string? Usr_Mod)
        {
            var valores = new EliminarMedioAmbienteEntity
            {
                Medio_Ambiente_Id = Medio_Ambiente_Id,
                Usr_Mod = Usr_Mod
            };

            var result = await _inspeccionesService.EliminarMedioAmbiente(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("deleteEliminarWeReport")]
        public async Task<IActionResult> EliminarWeReport(int We_Report_Id)
        {
            var valores = new EliminarWeReportEntity
            {
                We_Report_Id = We_Report_Id
            };

            var result = await _inspeccionesService.EliminarWeReport(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        private static async Task<string> GuardarArchivosAsync(List<IFormFile>? archivos, string carpeta, string prefijo)
        {
            if (archivos is null || archivos.Count == 0)
            {
                return string.Empty;
            }

            var rutas = new List<string>();

            foreach (var archivo in archivos)
            {
                if (archivo is null || archivo.Length <= 0)
                {
                    continue;
                }

                var extension = Path.GetExtension(archivo.FileName);
                if (string.IsNullOrWhiteSpace(extension))
                {
                    extension = ".jpg";
                }

                var nombreArchivo = $"{prefijo}_{DateTime.Now:yyyyMMdd_HHmmssfff}_{Guid.NewGuid():N}{extension}";
                var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                await using var stream = new FileStream(rutaCompleta, FileMode.Create, FileAccess.Write, FileShare.None);
                await archivo.CopyToAsync(stream);
                rutas.Add(rutaCompleta);
            }

            return string.Join(" | ", rutas);
        }

        private static string NormalizarMarca(string? valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return "N";
            }

            var limpio = valor.Trim().ToUpperInvariant();
            return limpio.StartsWith("S") ? "S" : "N";
        }
    }
}