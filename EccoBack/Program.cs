using EccoBack.Abstraction;
using EccoBack.Entities;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using EccoBack.Services.ROM.ENTEL_RETAIL.MGM_Products;
using EccoBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using EccoBack.Abstraction.Automapper;
using EccoBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports;
using EccoBack.Services.ROM.ENTEL_RETAIL.MGM_Reports;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using EccoBack.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(
    policy =>
    {
        policy.WithOrigins("http://localhost/", "https://localhost/",
            "http://localhost:58893", "https://biweb.grupotawa.com",
            " https://biweb.grupotawa.com:443/", "http://biweb.grupotawa.com/",
            "http://biweb.grupotawa.com:443/", "http://localhost:4200",
            "http://localhost:7117", "https://intranet.grupotawa.com/incentivosrom",
            "http://192.168.8.39:4200", "http://192.168.8.39",
            "http://192.168.8.39:4200/"
            ).AllowAnyHeader()
             .AllowAnyMethod();
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// En ConfigureServices en Startup.cs

builder.Services.AddTransient<DataAcces>();
IMapper mapper = AutoMapperConfig.Initialize();

//builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
//builder.Services.AddScoped<IProductoServices, ProductoServices>();

//builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
//builder.Services.AddScoped<IReportsServices, ReportsServices>();

//builder.Services.AddTransient<DependencyInjectionConfig>();

//builder.Services.AddTransient<RepositorySearchService>();

//SearchRepositoryService.prueba();

//RepositorySearchService.ListarInterfacesEImplementaciones();
//RepositorySearchService.ListarRepositoryEImplementaciones();

RepositorySearchService.RegistrarRepository(builder.Services);
RepositorySearchService.RegistrarServices(builder.Services);



builder.Services.AddSingleton(mapper);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECCONET TEST", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = builder.Configuration["JWT:Issuer"],
        //ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))

    };
});


//DependencyInjectionConfig.ScanAndConfigureDependencies(builder.Services);


var app = builder.Build();

//var repositorySearchService = app.Services.GetService<RepositorySearchService>();

//if (repositorySearchService != null)
//{
//    var repositories = repositorySearchService.GetRepositories();

//    // Puedes hacer lo que necesites con las instancias de los repositorios

//    Console.WriteLine("Operación realizada con éxito");
//}
//else
//{
//    Console.WriteLine("No se pudo obtener el servicio RepositorySearchService");
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseCors();


app.UseAuthorization();

app.MapControllers();

app.Run();
