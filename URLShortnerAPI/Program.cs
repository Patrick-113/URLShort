using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using URLShortnerAPI.Database;
using URLShortnerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("URLShortnerCs");
builder.Services.AddDbContext<UrlDbContext>(o => o.UseNpgsql(connectionString));

builder.Services.AddScoped<URLServiceLayer>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "URLShortnerAPI",
        Version = "v1"
    });

    var xmlPath = Path.Combine(AppContext.BaseDirectory, "URLShortnerAPI.xml");
    c.IncludeXmlComments(xmlPath);
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