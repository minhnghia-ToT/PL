using Microsoft.EntityFrameworkCore;
using PFL_API.Data;
using PFL_API.Services.Interfaces;
/*using PFL_API.Services.Implementations;*/

var builder = WebApplication.CreateBuilder(args);

// =======================
// ADD SERVICES
// =======================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =======================
// CORS (BẮT BUỘC CHO FE)
// =======================

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// =======================
// DB CONTEXT
// =======================

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// =======================
// DEPENDENCY INJECTION
// =======================

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

var app = builder.Build();

// =======================
// MIDDLEWARE PIPELINE
// =======================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⚠️ CORS PHẢI ĐẶT TRƯỚC AUTH
app.UseCors("AllowFrontend");

// app.UseAuthentication(); // chưa dùng
app.UseAuthorization();

app.MapControllers();

app.Run();
