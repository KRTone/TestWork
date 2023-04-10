using Producer.Domain.Aggregates.UserAggregate;
using Producer.WebApi.Utils.Application;
using Producer.WebApi.Utils.Infrastructure;
using Serilog;
using UserDtos;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.WriteTo.Console();
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .RegisterMediator()
    .RegisterApplication()
    .AddMasstransit()
    .AddAutoMapper(cfg =>
    {
        cfg.CreateMap<User, CreateUser>();
    });

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
