using AutoMapper;
using IntegraCTE.API.Workers;
using IntegraCTE.Core.Context;
using IntegraCTE.Core.UseCases;
using IntegraCTE.Infra.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var config = new MapperConfiguration(cfg =>
{
    //cfg.AddProfile<PropostaAdesaoProfile>();
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

builder.Services.AddTransient<ProcessarXMLCTE>();
builder.Services.AddHostedService<WorkerProcessamentoXML>();

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
