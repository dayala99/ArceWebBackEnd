using Arce.Web.Entity.Inspecciones;
using Arce.Web.Service;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Arce.Web.Api.Controllers.Inspecciones;

[ApiController]
[Route("api/Inspecciones")]
public class StopWorkController : ControllerBase
{
    private readonly IInspeccionesService _inspeccionesService;
    private readonly string _connectionString;

    public StopWorkController(IInspeccionesService inspeccionesService, IConfiguration configuration)
    {
        _inspeccionesService = inspeccionesService;
        _connectionString = configuration.GetConnectionString("Connection")!;
    }
        [HttpGet]
        [Route("getMostrarStopReport")]
        public async Task<IActionResult> MostrarStopReport(int Stop_Work_Id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            const string sql = @"
                SELECT
                    sw.Stop_Work_Id,
                    sw.We_Report_Cod AS We_Report_Cod,
                    sw.We_Report_Cod AS Codigo_We_Report,
                    sw.Codigo_Stop_Work AS Codigo_Stop_Work,
                    sw.Usr_Cod,
                    usr.Usr_Nom,
                    car.Cargo_Nombre,
                    cen.Cen_Cos_Des,
                    sw.Cliente_Id,
                    cli.Cliente_Nombre,
                    sw.Subestacion_Id,
                    sub.Subestacion_Nombre,
                    sw.Stop_Supervisor AS Supervisor_Cod,
                    sup.Usr_Nom AS Supervisor_Nom,
                    sw.Stop_Inspector AS Inspector_Cod,
                    sw.Stop_Inspector AS Inspector_Nom,
                    sw.Stop_OT AS OT,
                    sw.Stop_Trabajo AS Trabajo_Asignado,
                    sw.Stop_Procedimiento AS Procedimiento_Trabajo,
                    sw.Tipo_Riesgo_Id,
                    tr.Tipo_Riesgo,
                    sw.Estado
                FROM Ins_Stop_Work sw
                LEFT JOIN Sg_Usuario usr ON sw.Usr_Cod = usr.Usr_Cod
                LEFT JOIN Ins_Cargo car ON usr.Usr_Crg = car.Cargo_Id
                LEFT JOIN Lg_Cen_Cos cen ON usr.Usr_Cen_Cos_Id = cen.Cen_Cos_Id
                LEFT JOIN Ins_Cliente cli ON sw.Cliente_Id = cli.Cliente_Id
                LEFT JOIN Ins_SubEstacion sub ON sw.Subestacion_Id = sub.Subestacion_Id
                LEFT JOIN Sg_Usuario sup ON sw.Stop_Supervisor = sup.Usr_Cod
                LEFT JOIN Ins_Tipo_Riesgo tr ON sw.Tipo_Riesgo_Id = tr.Tipo_Riesgo_Id
                WHERE sw.Stop_Work_Id = @Stop_Work_Id";

            var datos = (await connection.QueryAsync<StopReportDetalleDto>(sql, new { Stop_Work_Id })).ToList();
            return Ok(datos);
        }

        [HttpGet]
        [Route("getFiltrarStopReport")]
        public async Task<IActionResult> FiltrarStopReport(DateTime? Fecha_Desde, DateTime? Fecha_Hasta, string? Estado)
        {
            var result = await _inspeccionesService.FiltrarStopReport(Fecha_Desde, Fecha_Hasta, Estado);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPost]
        [Route("postInsertarStopReport")]
        public async Task<IActionResult> InsertarStopReport([FromBody] InsStopReportEntity valores)
        {
            var result = await _inspeccionesService.InsertarStopReport(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        [HttpPut]
        [Route("putActualizarStopReport")]
        public async Task<IActionResult> ActualizarStopReport([FromBody] ActualizarStopReportEntity valores)
        {
            var result = await _inspeccionesService.ActualizarStopReport(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        
        [HttpDelete]
        [Route("deleteEliminarStopReport")]
        public async Task<IActionResult> EliminarStopReport([FromQuery] int Stop_Work_Id, [FromQuery] string? Usr_Mod)
        {
            if (Stop_Work_Id <= 0)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = "No se recibió un identificador válido para eliminar el Stop Work."
                });
            }

            var valores = new EliminarStopReportEntity
            {
                Stop_Work_Id = Stop_Work_Id,
                Usr_Mod = Usr_Mod
            };

            var result = await _inspeccionesService.EliminarStopReport(valores);
            if (result!.Success)
            {
                result.CodeResult = StatusCodes.Status200OK;
                return Ok(result);
            }

            result.CodeResult = StatusCodes.Status400BadRequest;
            return BadRequest(result);
        }

        private sealed class StopReportDetalleDto
        {
            public int? Stop_Work_Id { get; set; }
            public string? We_Report_Cod { get; set; }
            public string? Codigo_We_Report { get; set; }
            public string? Codigo_Stop_Work { get; set; }
            public string? Usr_Cod { get; set; }
            public string? Usr_Nom { get; set; }
            public string? Cargo_Nombre { get; set; }
            public string? Cen_Cos_Des { get; set; }
            public int? Cliente_Id { get; set; }
            public string? Cliente_Nombre { get; set; }
            public int? Subestacion_Id { get; set; }
            public string? Subestacion_Nombre { get; set; }
            public string? Supervisor_Cod { get; set; }
            public string? Supervisor_Nom { get; set; }
            public string? Inspector_Cod { get; set; }
            public string? Inspector_Nom { get; set; }
            public string? OT { get; set; }
            public string? Trabajo_Asignado { get; set; }
            public string? Procedimiento_Trabajo { get; set; }
            public int? Tipo_Riesgo_Id { get; set; }
            public string? Tipo_Riesgo { get; set; }
            public string? Estado { get; set; }
        }
}
