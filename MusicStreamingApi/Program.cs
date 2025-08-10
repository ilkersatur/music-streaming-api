using MusicStreamingApi.Models;
using MusicStreamingApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind FileSettings
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));

// Register Services
builder.Services.AddScoped<IPlayStatisticsService, PlayStatisticsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();