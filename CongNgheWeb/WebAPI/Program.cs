using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using WebAPI.HealthCheck;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((context, config) => config.ReadFrom.Configuration(context.Configuration));
// Configure database context
builder.Services.AddDbContext<QltsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})


.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogWarning("Authentication failed: {Message}", context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Token validated successfully for user {UserId}", context.Principal?.Identity?.Name);
            return Task.CompletedTask;
        }
    };
});

// Configure CORS to allow requests from a specific origin (e.g., localhost:3000 for development)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Register repositories and services
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<ITaiSanRepository, TaiSanRepository>();
builder.Services.AddScoped<IPhanBoTaiSanRepository, PhanBoTaiSanRepository>();
builder.Services.AddScoped<IBaoTriRepository, BaoTriRepository>();
builder.Services.AddScoped<IPhongRepository, PhongRepository>();
builder.Services.AddScoped<ILoaiTaiSanRepository, LoaiTaiSanRepository>();

builder.Services.AddScoped<BaoTriServices>();
builder.Services.AddScoped<NguoiDungServices>();
builder.Services.AddScoped<LoaiTaiSanServices>();
builder.Services.AddScoped<PhanBoTaiSanServices>();
builder.Services.AddScoped<PhongServices>();
builder.Services.AddScoped<TaiSanServices>();
builder.Services.AddScoped<AuthServices>();

builder.Services.AddHttpClient<ApiHealthCheck>();
builder.Services.AddHealthChecks()
    .AddCheck("SQL Database", new SqlConnectionHealthCheck(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddCheck<ApiHealthCheck>(nameof(ApiHealthCheck))
    .AddDbContextCheck<QltsContext>()
    .AddCheck<SystemHealthCheck>(nameof(SystemHealthCheck));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors("AllowLocalhost3000");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                exception = entry.Value.Exception?.Message,
                duration = entry.Value.Duration.ToString()
            })
        });
        await context.Response.WriteAsync(result);
    }
});
app.UseStaticFiles();


app.Run();
