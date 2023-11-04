using AutoMapper;
using IntegraCTE.API.Workers;
using IntegraCTE.Core.Context;
using IntegraCTE.Core.MapProfiles;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.UseCases;
using IntegraCTE.Infra.Context;
using IntegraCTE.Infra.Repository;
using IntegraCTE.Infra.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using System.Net.Http;
using IntegraCTE.Core.ValidationMessages;
using IntegraCTE.Infra.ValidationMessages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<ArquivoProfile>();
    cfg.AddProfile<CTEProfile>();
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddDbContext<IIntegraCTEContext, IntegraCTEContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("CTEConnection"), p =>
    {
        p.EnableRetryOnFailure(
             maxRetryCount: 2,
             maxRetryDelay: TimeSpan.FromSeconds(5),
             errorNumbersToAdd: null)
         .MigrationsHistoryTable("EFHistory_IntegraCTE");

    });
    o.EnableDetailedErrors();
    o.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);

builder.Services.AddHttpClient<ODataJson>((op) =>
{
    try
    {
        string aadClientAppId = builder.Configuration.GetSection("ERPService:ClientIdDynamics").Value;
        string aadClientSecret = builder.Configuration.GetSection("ERPService:ClientSecret").Value;
        string aadResource = builder.Configuration.GetSection("ERPService:UrlResource").Value;
        string aadTenant = builder.Configuration.GetSection("ERPService:ActiveDirectoryTenant").Value;

        AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant, false);
        AuthenticationResult authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, new ClientCredential(aadClientAppId, aadClientSecret)).Result;
        op.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationResult.AccessToken);
        op.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    catch (Exception ex)
    {
    }
    finally 
    { 
        op.Timeout = TimeSpan.FromMinutes(20);
        op.BaseAddress = new Uri(builder.Configuration.GetSection("ERPService:UrlResource").Value);
    }

});

builder.Services.AddScoped<IValidationMessage, ValidationMessage>();

builder.Services.AddTransient<IIntegraCTERepository, IntegraCTERepository>();
builder.Services.AddTransient<IERPService, ERPService>();

builder.Services.AddTransient<ProcessarXMLCTE>();
builder.Services.AddTransient<UploadCTE>();
builder.Services.AddTransient<IntegrarCTE>();

builder.Services.AddHostedService<WorkerProcessamentoXML>();

builder.Services.AddResponseCaching();

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
