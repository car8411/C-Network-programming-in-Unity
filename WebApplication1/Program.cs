using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen; // Swagger 관련 네임스페이스

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// GameDbContext 등록 (SQL Server 연결 문자열 설정)
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlServer("Server=localhost;Database=GameDatabase;User Id=car84;Password=dkahsen5890!!;"));

// MongoService 등록
builder.Services.AddScoped<MongoService>();

// SqlService 등록
builder.Services.AddScoped<SqlService>();

// Swagger 서비스 등록
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
        c.RoutePrefix = string.Empty; // Swagger UI 기본 경로 설정
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
