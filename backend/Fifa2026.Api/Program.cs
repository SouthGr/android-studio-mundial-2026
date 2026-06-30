using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Fifa2026.Api.Data;
using Fifa2026.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Render asigna un puerto dinámico vía la variable de entorno PORT y espera que
// la app escuche en 0.0.0.0. En desarrollo local, sin esa variable, se usa el puerto
// configurado en launchSettings.json (5110) como hasta ahora.
var renderPort = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(renderPort))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{renderPort}");
}

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

// El frontend espera siempre { mensaje: "..." } en los errores 400,
// incluso cuando el rechazo viene de la validación automática de [ApiController].
builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(opts =>
{
    opts.InvalidModelStateResponseFactory = context =>
    {
        var primerError = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .FirstOrDefault() ?? "Datos inválidos.";
        return new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(new { mensaje = primerError });
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    var jwtScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Pegá el token JWT obtenido en /api/auth/login (sin el prefijo 'Bearer ').",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    opts.AddSecurityDefinition("Bearer", jwtScheme);
    opts.AddSecurityRequirement(new OpenApiSecurityRequirement { { jwtScheme, Array.Empty<string>() } });
});

// Render expone la base de datos de Postgres como variable de entorno DATABASE_URL
// con formato postgres://usuario:clave@host:puerto/basededatos. Si existe, tiene
// prioridad sobre la cadena de conexión de appsettings (que se usa en desarrollo local).
var connectionString = ConvertirDatabaseUrl(Environment.GetEnvironmentVariable("DATABASE_URL"))
    ?? builder.Configuration.GetConnectionString("Fifa2026");

builder.Services.AddDbContext<Fifa2026DbContext>(opts => opts.UseNpgsql(connectionString));

builder.Services.AddSingleton<TokenService>();

var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services
    .AddAuthentication(opts =>
    {
        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!)),
        };
    });

builder.Services.AddAuthorization();

const string CorsPolicy = "FrontendCors";
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(CorsPolicy, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Fifa2026DbContext>();
    DbInitializer.Seed(db);
}

// Swagger queda disponible siempre (también en Render) para poder probar la API
// desde el navegador sin instalar nada — es un proyecto académico, sin datos sensibles.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(CorsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Convierte la URL de conexión que entrega Render (postgres://usuario:clave@host:puerto/db)
// al formato de cadena de conexión que espera Npgsql (clave=valor separados por ';').
static string? ConvertirDatabaseUrl(string? databaseUrl)
{
    if (string.IsNullOrWhiteSpace(databaseUrl)) return null;

    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':', 2);

    return new Npgsql.NpgsqlConnectionStringBuilder
    {
        Host = uri.Host,
        Port = uri.Port > 0 ? uri.Port : 5432,
        Username = userInfo[0],
        Password = userInfo.Length > 1 ? userInfo[1] : "",
        Database = uri.AbsolutePath.TrimStart('/'),
        SslMode = Npgsql.SslMode.Require,
    }.ToString();
}
