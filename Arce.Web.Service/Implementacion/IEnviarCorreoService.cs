using Arce.Web.Entity;

namespace Arce.Web.Service;

public interface IEnviarCorreoService
{
     Task EnviarCorreoAsync(CorreoEntity correo);
}
