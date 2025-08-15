using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RunApp.Api.Middleware;
using RunApp.Infrastructure;
using RunnApp.Application;
using RunApp.Api.Services;
using RunApp.Infrastructure.Middleware;
using RunnApp.Application.Common.Authorization;
using RunApp.Infrastructure.Common.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection"), builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddTransient<IJwtServiceGenerator, JwtServiceGenerator>();
builder.Services.AddHttpContextAccessor();
builder.Logging.AddConsole();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Authentication / JWT
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    // Fail fast in a clear way (so logs show why)
    builder.Logging.AddConsole();
    Console.WriteLine("ERROR: Jwt:Key is missing from configuration.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? string.Empty)),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
    opt.IncludeErrorDetails = true;
});

// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "StoreProfile",
        policyBuilder => policyBuilder.RequireClaim("StoreProfile", "true"));

    options.AddPolicy(
        "ProductMustBeBoughtInOrderToBeRated",
        policyBuilder => policyBuilder.AddRequirements(new ProductMusBeBoughtRequirement())
    );
});

var app = builder.Build();

// Exception middleware first to capture startup errors
app.UseExceptionMiddleware(); // keep this very early

// Simple request logger to confirm app receives requests
app.Use(async (context, next) =>
{
    Console.WriteLine($"[REQ] {context.Request.Method} {context.Request.Path}");
    await next();
    Console.WriteLine($"[RES] {context.Response.StatusCode} {context.Request.Path}");
});

// Routing + CORS + Auth (order matters)
app.UseRouting();

app.UseCors("AllowAllOrigins");

// NOTE: temporarily disable DB-heavy middleware while debugging connectivity
 app.UseEventsInfrastructureMiddleware();

app.UseAuthentication();
app.UseAuthorization();

// A minimal health endpoint to test easily
app.MapGet("/ping", () => Results.Ok("pong"));

// Map controllers
app.MapControllers();

// If you want to keep it (but move it later after debugging), re-enable Events middleware
// app.UseEventsInfrastructureMiddleware();

app.Run();
