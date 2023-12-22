using Serilog;
using Test.Backend.Application;
using Test.Backend.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

//SeriLog
//Log.Logger = new LoggerConfiguration()
// .WriteTo.Console()
// .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
// .CreateLogger();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastuctureServices(builder.Configuration);

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp",
builder => builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
