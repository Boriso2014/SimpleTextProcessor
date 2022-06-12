
using SimpleTextProcessor.Services;
using SimpleTextProcessor.Services.Converter;
using SimpleTextProcessor.Services.Middleware;
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
builder.Services.AddTransient<IFileProcessWrapper, FileProcessWrapper>();
builder.Services.AddTransient<IFileInfoConverter, FileInfoConverter>();
builder.Services.AddTransient<ITextProcessor, TextProcessor>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(simpleTextProcessorOrigins);
app.UseExceptionHandling();

app.UseAuthorization();

app.MapControllers();

app.Run();
