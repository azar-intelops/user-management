using Application.Extensions;
using Application.Mappers;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/********************************************************************************************************/
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
/********************************************************************************************************/

//OpenTelemetry
const string serviceName = "UserService";
builder.Services.AddCustomOpenTelemetry(serviceName);
builder.Logging.AddCustomOpenTelemetryLogging(serviceName);
//Honeycomb
builder.Services.AddHoneycomb(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
