using Arce.Web.Entity;

namespace Arce.Web.Data;

public interface IClienteTjh2bRepository
{
    Task<IEnumerable<ClienteTjh2bEntity>?> ListarClienteTjh2b(int? Id, string? Nombre, string? Estado);
    Task<IEnumerable<ClienteTjh2bEntity>?> ConsultarDatosClienteTjh2b(int? Cliente_Id);
    Task<(int Codigo, string Mensaje)> RegistrarClienteTjh2b(ClienteTjh2bEntity valores);
    Task<(int Codigo, string Mensaje)> ActualizarClienteTjh2b(ClienteTjh2bEntity valores);
    Task<(int Codigo, string Mensaje)> EliminarClienteTjh2b(int? Id, string? Usr_Mod);
}
