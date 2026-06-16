using Arce.Web.Entity;
using Arce.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviarCorreoController : ControllerBase
    {
        private readonly IEnviarCorreoService _correoService;
        private readonly ILogger<EnviarCorreoController> _logger;

        public EnviarCorreoController(IEnviarCorreoService correoService, ILogger<EnviarCorreoController> logger)
        {
            _correoService = correoService;
            _logger = logger;
        }

        [HttpPost]
        [Route("postEnviarCorreo")]
        public async Task<IActionResult> EnviarCorreo([FromBody] CorreoEntity correo)
        {
            try
            {
                await _correoService.EnviarCorreoAsync(correo);

                return Ok(new
                {
                    Success = true,
                    Message = "Correo enviado correctamente."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enviando correo.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "No se pudo enviar el correo.",
                    Detail = ObtenerDetalleError(ex)
                });
            }
        }

        [HttpPost]
        [Route("postEnviarCorreoPedidoGenerado")]
        public async Task<IActionResult> EnviarCorreoPedidoGenerado([FromBody] CorreoPedidoGeneradoEntity valores)
        {
            try
            {
                var destino = string.IsNullOrWhiteSpace(valores.CorreoDestino)
                    ? "systemas@arceperu.pe"
                    : valores.CorreoDestino.Trim();

                var asunto = $"Pedido generado #{valores.Ped_Id}";

                var productosHtml = string.Join("", (valores.Productos ?? new List<CorreoPedidoGeneradoProductoEntity>())
                .Select(p => $"<li>{p.DescripcionProducto} - {p.DescripcionUnidad?.ToUpper()} - {p.CentroCosto} - Cantidad: {p.Cantidad}</li>"));

                var cuerpo = $@"
                    <h3>Pedido pendiente de aprobacion</h3>
                    <p>Se ha registrado el pedido: <strong>{valores.Ped_Id}</strong></p>
                    <p><strong>Tipo de Servicio:</strong> {valores.TipoServicio}</p>
                    <p><strong>Productos:</strong></p>
                    <ol>{productosHtml}</ol>
                    <p><strong>Usuario registro:</strong> {valores.UsuarioRegistro}</p>
                    <p><strong>Usuario aprobacion:</strong> {valores.UsuarioAprobacion}</p>
                    <p>
                        <strong>Intranet:</strong> 
                        <a href=""https://gestion.montajeseingenieriaarceperu.com/Arce/login"" target=""_blank"">
                            Ingresar a la intranet
                        </a>
                    </p>
                ";

                await _correoService.EnviarCorreoAsync(new CorreoEntity
                {
                    Para = destino,
                    Asunto = asunto,
                    CuerpoHtml = cuerpo
                });

                return Ok(new
                {
                    Success = true,
                    Message = "Correo de pedido generado enviado correctamente."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enviando correo de pedido generado {PedId}.", valores?.Ped_Id);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "No se pudo enviar el correo de pedido generado.",
                    Detail = ObtenerDetalleError(ex)
                });
            }

        }

        private static string ObtenerDetalleError(Exception ex)
        {
            var mensajes = new List<string>();
            var actual = ex;

            while (actual != null)
            {
                mensajes.Add(actual.Message);
                actual = actual.InnerException;
            }

            return string.Join(" | ", mensajes);
        }

        [HttpPost]
        [Route("postEnviarCorreoPedidoAprobado")]
        public async Task<IActionResult> EnviarCorreoPedidoAprobado([FromBody] CorreoPedidoAprobadoEntity valores)
        {
            try
            {
                var destino = string.IsNullOrWhiteSpace(valores.CorreoDestino)
                    ? "systemas@arceperu.pe"
                    : valores.CorreoDestino.Trim();

                var asunto = $"Pedido aprobado #{valores.Ped_Id}";

                var cuerpo = $@"
                    <h3>Pedido aprobado</h3>
                    <p>Se ha aprobado el pedido: <strong>{valores.Ped_Id}</strong></p>
                    <p><strong>Tipo de Servicio:</strong> {valores.TipoServicio}</p>
                    <p><strong>Usuario registro:</strong> {valores.UsuarioRegistro}</p>
                    <p><strong>Usuario aprobacion:</strong> {valores.UsuarioAprobacion}</p>
                    <p>
                        <strong>Intranet:</strong>
                        <a href=""https://gestion.montajeseingenieriaarceperu.com/Arce/login"">
                            Ingresar al sistema
                        </a>
                    </p>
                ";

                await _correoService.EnviarCorreoAsync(new CorreoEntity
                {
                    Para = destino,
                    Asunto = asunto,
                    CuerpoHtml = cuerpo
                });

                return Ok(new
                {
                    Success = true,
                    Message = "Correo de pedido aprobado enviado correctamente."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enviando correo de pedido aprobado {PedId}.", valores?.Ped_Id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "No se pudo enviar el correo de pedido aprobado.",
                    Detail = ObtenerDetalleError(ex)
                });
            }
        }

        [HttpPost]
        [Route("postEnviarCorreoPedidoRechazado")]
        public async Task<IActionResult> EnviarCorreoPedidoRechazado([FromBody] CorreoPedidoRechazadoEntity valores)
        {
            try
            {
                var destino = string.IsNullOrWhiteSpace(valores.CorreoDestino)
                    ? "systemas@arceperu.pe"
                    : valores.CorreoDestino.Trim();

                var asunto = $"Pedido rechazado #{valores.Ped_Id}";

                var cuerpo = $@"
                    <h3>Pedido rechazado</h3>
                    <p>Se ha rechazado el pedido: <strong>{valores.Ped_Id}</strong></p>
                    <p><strong>Tipo de Servicio:</strong> {valores.TipoServicio}</p>
                    <p><strong>Usuario registro:</strong> {valores.UsuarioRegistro}</p>
                    <p><strong>Usuario rechazo:</strong> {valores.UsuarioAprobacion}</p>
                    <p><strong>Motivo:</strong> {valores.MotivoRechazo}</p>
                    <p>
                        <strong>Intranet:</strong>
                        <a href=""https://gestion.montajeseingenieriaarceperu.com/Arce/login"">
                            Ingresar al sistema
                        </a>
                    </p>
                ";

                await _correoService.EnviarCorreoAsync(new CorreoEntity
                {
                    Para = destino,
                    Asunto = asunto,
                    CuerpoHtml = cuerpo
                });

                return Ok(new
                {
                    Success = true,
                    Message = "Correo de pedido rechazado enviado correctamente."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enviando correo de pedido rechazado {PedId}.", valores?.Ped_Id);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Success = false,
                    Message = "No se pudo enviar el correo de pedido rechazado.",
                    Detail = ObtenerDetalleError(ex)
                });
            }
        }
    }
}
