using Arce.Web.Data;
using Arce.Web.Entity;
using Arce.Web.Service.Comunes;

namespace Arce.Web.Service;

public class PedidoService: IPedidoService
{
    private readonly IPedidoRepository _repository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _repository = pedidoRepository;
    }
    public async Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedido(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com, string? Usr_Cod)
    {
        var result = new ServiceResponseList<PedidoCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarPedido(Ped_Id, Flg_Est, Ped_Tip_Com, Usr_Cod);

            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoCorrelativoNuevo()
    {
        var result = new ServiceResponseList<PedidoCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarPedidoCorrelativoNuevo();
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoModificar(int Ped_Id)
    {
        var result = new ServiceResponseList<PedidoCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarPedidoModificar(Ped_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarPedido(PedidoCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarPedido(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarPedido(PedidoCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarPedido(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarPedidoEstado(PedidoCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarPedidoEstado(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCosto(int? Ped_Id)
    {
        var result = new ServiceResponseList<PedidoCabeceraCentroCostoEntity>();
        try
        {
            var resultData = await _repository.ListarPedidoRegistradoCentroCosto(Ped_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraCentroCostoEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ListarPedidoRegistradoCentroCostoModificar(int? Ped_Cen_Cos_Id)
    {
        var result = new ServiceResponseList<PedidoCabeceraCentroCostoEntity>();
        try
        {
            var resultData = await _repository.ListarPedidoRegistradoCentroCostoModificar(Ped_Cen_Cos_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraCentroCostoEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarCentroCostoPedidoRegistrado(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> EliminarCentroCostoPedidoRegistrado(PedidoCabeceraCentroCostoEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.EliminarCentroCostoPedidoRegistrado(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraCentroCostoEntity>?> ObtenerTotalPedidoPorCenCos(int Ped_Id, string Ped_Cen_Cos)
    {
        var result = new ServiceResponseList<PedidoCabeceraCentroCostoEntity>();
        try
        {
            var resultData = await _repository.ObtenerTotalPedidoPorCenCos(Ped_Id, Ped_Cen_Cos);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraCentroCostoEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetallePedido(int Ped_Cab_Id)
    {
        var result = new ServiceResponseList<PedidoDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarDetallePedido(Ped_Cab_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoDetalleEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetallePedidoModificar(int Ped_Det_Id)
    {
        var result = new ServiceResponseList<PedidoDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarDetallePedidoModificar(Ped_Det_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoDetalleEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> RegistrarDetallePedido(PedidoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.RegistrarDetallePedido(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarDetallePedido(PedidoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarDetallePedido(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> EliminarDetallePedido(PedidoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.EliminarDetallePedido(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> AsignarOrdenCompra(PedidoCabeceraCentroCostoEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.AsignarOrdenCompra(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> AsignarOrdenCompraADetallePedido(PedidoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.AsignarOrdenCompraADetallePedido(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoDetalleEntity>?> ListarItemsAsignadosPedidoCentroCosto(int Ped_Cab_Id)
    {
        var result = new ServiceResponseList<PedidoDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarItemsAsignadosPedidoCentroCosto(Ped_Cab_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoDetalleEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoDetalleEntity>?> ListarItemsAsignadosPedidoCentroCostoModificar(int Ord_Com_Id, int Ped_Cab_Id)
    {
        var result = new ServiceResponseList<PedidoDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarItemsAsignadosPedidoCentroCostoModificar(Ord_Com_Id, Ped_Cab_Id);
            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoDetalleEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraEntity>?> CargarReportePedido(string Ped_Id)
    {
        var result = new ServiceResponseList<PedidoCabeceraEntity>();
        try
        {
            var resultData = await _repository.CargarReportePedido(Ped_Id);

            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> DesAsignarOrdenCompraADetallePedido(PedidoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.DesAsignarOrdenCompraADetallePedido(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoCabeceraEntity>?> ListarPedidoAprobadoParaOC(int? Ped_Id, string? Flg_Est, int? Ped_Tip_Com)
    {
        var result = new ServiceResponseList<PedidoCabeceraEntity>();
        try
        {
            var resultData = await _repository.ListarPedidoAprobadoParaOC(Ped_Id, Flg_Est, Ped_Tip_Com);

            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoCabeceraEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarPedidoCuandoDetalleCompleto(PedidoCabeceraEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarPedidoCuandoDetalleCompleto(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponseList<PedidoDetalleEntity>?> ListarDetalleIngresoAlmacen(int? Ped_Cab_Id, int? Ord_Com_Id)
    {
        var result = new ServiceResponseList<PedidoDetalleEntity>();
        try
        {
            var resultData = await _repository.ListarDetalleIngresoAlmacen(Ped_Cab_Id, Ord_Com_Id);

            if (resultData == null || !resultData.Any())
            {
                result.Success = true;
                result.Message = "No existe información";
                result.Elements = new List<PedidoDetalleEntity>();
                result.TotalElements = 0;
                return result;
            }

            var elementos = resultData.ToList();

            result.Success = true;
            result.Message = "Completado con éxito";
            result.Elements = elementos;
            result.TotalElements = elementos.Count;
            return result;
        }
        catch (Exception ex)
        {
            result.Message = "Excepción no controlada " + ex.Message;
            return result;
        }
    }

    public async Task<ServiceResponse<int>> ActualizarPedidoDetalleIngresoAlmacen(PedidoDetalleEntity valores)
    {
        var result = new ServiceResponse<int>();
        try
        {
            var resultData = await _repository.ActualizarPedidoDetalleIngresoAlmacen(valores);
            if (resultData.Codigo == 0)
            {
                result.Success = true;
                result.Message = resultData.Mensaje;
                result.CodeTransacc = resultData.Codigo;
                return result;
            }
            result.Success = false;
            result.Message = resultData.Mensaje;
            return result;
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Message = "Error inesperado " + ex.Message;
            return result;
        }
    }
}
