using FIAP.FCG.Transaction.API.Extensions;
using FIAP.FCG.Transaction.Domain.Entity;
using FIAP.FCG.Transaction.Infrastructure.Repository;
using FIAP.FCG.Transaction.Service.Clients;
using FIAP.FCG.Transaction.Service.Interfaces.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region -- Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Fiap Cloud Games",
        Version = "v1",
        Description = "API criada para validação dos entregaveis do curso POS-TECH Arquitetura de Sistemas .NET com Azure"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite o token JWT sem 'Bearer '"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    var serviceXml = Path.Combine(AppContext.BaseDirectory, "FIAP.FCG.Transaction.Service.xml");
    if (File.Exists(serviceXml))
        c.IncludeXmlComments(serviceXml);
});

#endregion

#region -- Data

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
}, ServiceLifetime.Scoped);

#endregion

#region -- JWT

builder.Services.UseJwtAuthentication(builder.Configuration);

#endregion

#region -- DI

builder.Services.AddCorrelationIdGenerator();
builder.Services.AddRepositoryDI();
builder.Services.AddServiceDI();

builder.Services.Configure<PaymentLambdaOptions>(
    builder.Configuration.GetSection("PaymentLambda"));

builder.Services.AddHttpClient<IUserClient, UserClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:User:BaseUrl"]!);
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddHttpClient<IGameClient, GameClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:Game:BaseUrl"]!);
    client.Timeout = TimeSpan.FromSeconds(60);
});


#endregion

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddHealthChecks();

#region -- Datadog
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.DatadogLogs(
                apiKey: configuration["Datadog:ApiKey"],
                source: "dotnet-application",
                service: "fiap-fcg-transaction",
                host: "fiap-fcg-transaction",
                tags: new[] { "env:homolog", "version:1.0.0" }
            )
            .CreateLogger();

builder.Host.UseSerilog();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region -- Middleware

app.UseCorrelationMiddleware();
app.UseLogMiddleware();
app.UseGlobalExceptionHandler();

#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
