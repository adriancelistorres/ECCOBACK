using RombiBack.Abstraction;
using AutoMapper;
using RombiBack.Entities;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Products;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Products;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EccoBack.Abstraction.Automapper;
using RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Reports;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using RombiBack.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using RombiBack.Security.Auth.Repsitory;
using RombiBack.Security.Auth.Services;
using RombiBack.Security.JWT;

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
            "http://192.168.8.39:4200", "http://192.168.8.39", "https://intranet.grupotawa.com/RomBIBack",
            "http://192.168.8.39:4200/", "https://intranet.grupotawa.com", "https://intranet.grupotawa.com/RomBI",
            "https://intranet.grupotawa.com/RomBI/authIn/loginOne"
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

//AUTOMAPER ANTIGUO
IMapper mapper = AutoMapperConfig.Initialize();

//AUTOMAPER ANTIGUO

builder.Services.AddSingleton(mapper);

//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(Program));


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = "MyCacheInstance"; // Nombre de la instancia
    options.Configuration = "localhost:6379,password=adrianc"; // Configuración de conexión
});


builder.Services.AddHttpClient();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IGenerateToken, GenerateTokenMain>();


//builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
//builder.Services.AddScoped<IReportsServices, ReportsServices>();

//builder.Services.AddTransient<DependencyInjectionConfig>();

//builder.Services.AddTransient<RepositorySearchService>();

//SearchRepositoryService.prueba();

//RepositorySearchService.ListarInterfacesEImplementaciones();
//RepositorySearchService.ListarRepositoryEImplementaciones();

RepositorySearchService.RegistrarRepository(builder.Services);
RepositorySearchService.RegistrarServices(builder.Services);



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
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
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
