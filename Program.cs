using CarKilometerTrack.AppDbConnect;
using CarKilometerTrack.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetEnv;
using System.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;

var envPath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
if (File.Exists(envPath))
{
    Env.Load(envPath);
}

var builder = WebApplication.CreateBuilder(args);

if (File.Exists(envPath))
{
    Env.Load(envPath);
    Console.WriteLine($".env yüklendi: {envPath}");
}
else
{
    Console.WriteLine($".env bulunamadý: {envPath}");
}

var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
string? connStr = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
string? jwtKey = Environment.GetEnvironmentVariable("JWT__KEY");
string? jwtIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER");
string? jwtAudience = Environment.GetEnvironmentVariable("JWT__AUDIENCE");
string corsOrigins = Environment.GetEnvironmentVariable("CORS_ORIGINS") ?? "";
Console.WriteLine(corsOrigins);
var origins = corsOrigins.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
int accessMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWT_ACCESS_MINUTES"), out var am) ? am : 10;

JwtHelper._secretKey = jwtKey;
JwtHelper._issuer = jwtIssuer;
JwtHelper._audience = jwtAudience;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppServices();

builder.Services.AddAutoMapper(typeof(AutoMapperHelper).Assembly);

var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)),
        ClockSkew = TimeSpan.FromMinutes(1)

    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("SpaCors", policy =>
    {
        policy.WithOrigins(origins) 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connStr));

var app = builder.Build();

app.UseCors("SpaCors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedFor
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
