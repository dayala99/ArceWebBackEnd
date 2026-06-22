using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IClienteRepository
{
    Task<IEnumerable<ClienteEntity>?> ListarCliente(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<ClienteEntity>?> ConsultarDatosCliente(int? Cliente_Id);
    Task<(int Codigo, string Mensaje)> RegistrarCliente(ClienteEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarCliente(ClienteEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarCliente(int? Id, string? Usr_Mod);
}
