using Arce.Web.Data;
using Arce.Web.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        /*DESARROLLO*/

        policy.WithOrigins(
                    "http://localhost:4200", 
                    "https://localhost:4200"
                    )  // Especifica el origen permitido
                .AllowAnyHeader()                     // Permitir cualquier encabezado
                .AllowAnyMethod()                   // Permitir cualquier método (GET, POST, etc.)
                .AllowCredentials();

        /*PRODUCCION*/

        // policy.WithOrigins(
        // // "http://192.168.1.36",
        // // "https://192.168.1.36",
        // "https://gestion.montajeseingenieriaarceperu.com",
        // "https://gestion.montajeseingenieriaarceperu.com:443"
        // )  // Especifica el origen permitido
        // .AllowAnyHeader()                     // Permitir cualquier encabezado
        // .AllowAnyMethod();                   // Permitir cualquier método (GET, POST, etc.) 

    }); 
});

#region INYECTION DEPENDECY

//Inyection Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IFormaPagoService, FormaPagoService>();
builder.Services.AddScoped<ITipoServicioService, TipoServicioService>();
builder.Services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();
builder.Services.AddScoped<ICentroCostoService, CentroCostoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IGrupoItemService, GrupoItemService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IBancoService, BancoService>();
builder.Services.AddScoped<IMonedaService, MonedaService>();
builder.Services.AddScoped<IOrdenCompraService, OrdenCompraService>();
builder.Services.AddScoped<IAlmacenService, AlmacenService>();
builder.Services.AddScoped<IDetraccionService, DetraccionService>();
builder.Services.AddScoped<ISubGrupoItemService, SubGrupoItemService>();
builder.Services.AddScoped<IItemDetalleMaterialService, ItemDetalleMaterialService>();
builder.Services.AddScoped<IUbicacionService, UbicacionService>();

builder.Services.AddScoped<IInspeccionesService, InspeccionesService>();

builder.Services.AddScoped<IDireccionEntregaService, DireccionEntregaService>();
builder.Services.AddScoped<IEnviarCorreoService, EnviarCorreoService>();
builder.Services.AddScoped<IPerfilService, PerfilService>();
builder.Services.AddScoped<IAccesoService, AccesoService>();

//Inyection Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IFormaPagoRepository, FormaPagoRepository>();
builder.Services.AddScoped<ITipoServicioRepository, TipoServicioRepository>();
builder.Services.AddScoped<IUnidadMedidaRepository, UnidadMedidaRepository>();
builder.Services.AddScoped<ICentroCostoRepository, CentroCostoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IGrupoItemRepository, GrupoItemRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IBancoRepository, BancoRepository>();
builder.Services.AddScoped<IMonedaRepository, MonedaRepository>();
builder.Services.AddScoped<IOrdenCompraRepository, OrdenCompraRepository>();
builder.Services.AddScoped<IAlmacenRepository, AlmacenRepository>();
builder.Services.AddScoped<IDetraccionRepository, DetraccionRepository>();
builder.Services.AddScoped<ISubGrupoItemRepository, SubGrupoItemRepository>();
builder.Services.AddScoped<IItemDetalleMaterialRepository, ItemDetalleMaterialRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();

builder.Services.AddScoped<IInspeccionesRepository, InspeccionesRepository>();

builder.Services.AddScoped<IDireccionEntregaRepository, DireccionEntregaRepository>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();
builder.Services.AddScoped<IAccesoRepository, AccesoRepository>();

#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Abrir Swagger automáticamente en el navegador
    var swaggerUrl = "http://localhost:5218/swagger/index.html";
    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
        FileName = swaggerUrl,
        UseShellExecute = true
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();