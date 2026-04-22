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

        policy.WithOrigins("http://localhost:4200")  // Especifica el origen permitido
              .AllowAnyHeader()                     // Permitir cualquier encabezado
              .AllowAnyMethod();                   // Permitir cualquier m�todo (GET, POST, etc.)


        /*PRODUCCION*/

        //policy.WithOrigins(
        //"http://192.168.1.36",
        //"https://192.168.1.36",
        //"https://gestion.precotex.com",
        //"https://gestion.precotex.com:444"
        //)  // Especifica el origen permitido
        //.AllowAnyHeader()                     // Permitir cualquier encabezado
        //.AllowAnyMethod();                   // Permitir cualquier m�todo (GET, POST, etc.) 

    }); 
});

#region INYECTION DEPENDECY

//Inyection Services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();

//Inyection Repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();


#endregion




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
