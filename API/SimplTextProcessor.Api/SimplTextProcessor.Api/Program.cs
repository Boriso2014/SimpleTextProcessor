using System.IO.Abstractions;
using SimpleTextProcessor.Services;
using SimpleTextProcessor.Infrastructure.Converter;
using SimpleTextProcessor.Infrastructure.Middleware;
using SimpleTextProcessor.Services.Wrapper;

var simpleTextProcessorOrigins = "SimpleTextProcessorOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: simpleTextProcessorOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddSingleton<IFileSystem, FileSystem>();
builder.Services.AddSingleton<IFileProcessWrapper, FileProcessWrapper>();
builder.Services.AddSingleton<IFileInfoConverter, FileInfoConverter>();
builder.Services.AddScoped<ITextProcessor, TextProcessor>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(simpleTextProcessorOrigins);
app.UseExceptionHandling();

app.UseAuthorization();

app.MapControllers();

app.Run();
