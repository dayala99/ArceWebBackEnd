namespace Arce.Web.Entity;

public class CorreoEntity
{
    public string? Para { get; set; }
    public string? Asunto { get; set; }
    public string? CuerpoHtml { get; set; }
    public string? CuerpoTexto { get; set; }
    public List<string>? Copias { get; set; }
    public List<string>? CopiasOcultas { get; set; }
}

public class CorreoPedidoGeneradoEntity
{
    public int Ped_Id { get; set; }
    public string? CorreoDestino { get; set; }
    public string? UsuarioRegistro { get; set; }
    public string? Referencia { get; set; }
    public string? UsuarioAprobacion { get; set; }
    public string? TipoServicio { get; set; }
    public List<CorreoPedidoGeneradoProductoEntity>? Productos { get; set; }
}

public class CorreoPedidoGeneradoProductoEntity
{
    public int Item { get; set; }
    public string? DescripcionProducto { get; set; }
    public string? DescripcionUnidad { get; set; }
    public string? CentroCosto { get; set; }
    public decimal Cantidad { get; set; }
}

public class CorreoPedidoAprobadoEntity
{
    public int Ped_Id { get; set; }
    public string? CorreoDestino { get; set; }
    public string? UsuarioRegistro { get; set; }
    public string? UsuarioAprobacion { get; set; }
    public string? TipoServicio { get; set; }
}

public class CorreoPedidoRechazadoEntity
{
    public int Ped_Id { get; set; }
    public string? CorreoDestino { get; set; }
    public string? UsuarioRegistro { get; set; }
    public string? UsuarioAprobacion { get; set; }
    public string? TipoServicio { get; set; }
    public string? MotivoRechazo { get; set; }
}