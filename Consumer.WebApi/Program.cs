using Consumer.Application.Commands;
using Consumer.Domain.Aggregates.UserAggregate;
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
        cfg.CreateMap<User, Consumer.WebApi.ViewModels.User>()
            .ForCtorParam(nameof(Consumer.WebApi.ViewModels.User.guid), opt => opt.MapFrom(s => s.Guid))
            .ForCtorParam(nameof(Consumer.WebApi.ViewModels.User.phoneNumber), opt => opt.MapFrom(s => s.PhoneNumber))
            .ForCtorParam(nameof(Consumer.WebApi.ViewModels.User.email), opt => opt.MapFrom(s => s.Email))
            .ForCtorParam(nameof(Consumer.WebApi.ViewModels.User.name), opt => opt.MapFrom(s => s.Name))
            .ForCtorParam(nameof(Consumer.WebApi.ViewModels.User.lastName), opt => opt.MapFrom(s => s.LastName))
            .ForCtorParam(nameof(Consumer.WebApi.ViewModels.User.patronymic), opt => opt.MapFrom(s => s.Patronymic));
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

