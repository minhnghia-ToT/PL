using Microsoft.EntityFrameworkCore;
using PFL_API.Data;
using PFL_API.Services.Interfaces;
/*using PFL_API.Services.Implementations; */
// Remove or comment out this line if the Services namespace does not exist
// using PFL_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Add services
// =======================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =======================
// DbContext (SQL Server)
// =======================

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// =======================
// Dependency Injection
// =======================

// User
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

// =======================
// Logging (dev)
// =======================

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddLogging(logging =>
    {
        logging.AddConsole();
    });
}

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

// app.UseAuthentication(); // chưa dùng
app.UseAuthorization();

app.MapControllers();
app.Run();
