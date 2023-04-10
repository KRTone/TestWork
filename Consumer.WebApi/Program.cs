using Consumer.Application.Commands;
using Consumer.Infastructure.DataBase;
using Consumer.WebApi.Utils.Application;
using Consumer.WebApi.Utils.Infrastructure;
using UserDtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services
    .AddDbContext(builder.Configuration)
    .AddMasstransit()
    .RegisterApplication()
    .RegisterMediator()
    .AddAutoMapper(cfg =>
    {
        cfg.CreateMap<CreateUser, CreateUserCommand>();
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MigrateDbContext<ConsumerDbContext>((context, services) =>
    {
        var logger = services.GetRequiredService<ILogger<ConsumerContexSeed>>();
        new ConsumerContexSeed()
            .SeedAsync(context, logger)
            .Wait();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

