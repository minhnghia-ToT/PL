using Microsoft.EntityFrameworkCore;
using PFL_API.Data;
using PFL_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Add services
// =======================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===== DbContext (SQL Server) =====
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// (Tuỳ chọn) log SQL khi debug
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddLogging(logging =>
    {
        logging.AddConsole();
    });
}

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// =======================
// Configure pipeline
// =======================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ❌ Chưa dùng Authentication
// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
