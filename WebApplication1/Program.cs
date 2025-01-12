using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen; // Swagger ���� ���ӽ����̽�

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Kestrel ����
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(443, listenOptions =>
    {
        listenOptions.UseHttps("C:\\Windows\\System32\\jihunchja.com.crt", "C:\\Windows\\System32\\jihunchja.com.key");
    });
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// GameDbContext ��� (SQL Server ���� ���ڿ� ����)
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MongoService ���
builder.Services.AddScoped<MongoService>();

// SqlService ���
builder.Services.AddScoped<SqlService>();

// Swagger ���� ���
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Game API",
        Version = "v1",
        Description = "A sample API for managing game data in MongoDB and SQL Server"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Game API V1");
        c.RoutePrefix = "swagger"; // Swagger UI �⺻ ��� ����
    });
}

// HTTPS ���𷺼� Ȱ��ȭ
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();