using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TriadInterviewBackend.DataLayer.Contexts;
using TriadInterviewBackend.DataLayer.Repositories;
using TriadInterviewBackend.DomainLayer.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TriadInterviewBackend.DataLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dependency Injection for DB Context and Repositories
builder.Services.AddDbContext<TriadDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();


builder.Services.AddControllers();


builder.Services.AddAuthorization();


builder.Services.AddIdentityApiEndpoints<IdentityUserEntity>()
    .AddEntityFrameworkStores<UserDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ASP.NET Core Registration
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowReactApp", policy => {
        policy.WithOrigins("http://localhost:3000") // Your React URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// app.UseHttpsRedirection(); // Commented out for development to avoid redirect issues

app.UseCors("AllowReactApp");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<IdentityUserEntity>();

app.MapPost("/logout", async (SignInManager<IdentityUserEntity> signInManager) => {
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.MapGet("/getUserEmail", (ClaimsPrincipal user) => {
    var email = user.FindFirstValue(ClaimTypes.Email);
    return Results.Json(new {Email = email});
}).RequireAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();

