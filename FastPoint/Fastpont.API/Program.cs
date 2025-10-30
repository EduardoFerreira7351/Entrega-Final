// FastPoint/FastPoint.API/Program.cs
using FastPoint.API.Middlewares;
using FastPoint.Application.Interfaces;
using FastPoint.Application.Services;
using FastPoint.Domain.Enums; // Importando seu Enum 'PerfilUsuario'
using FastPoint.Infrastructure.Context; // Importando seu 'AppDbContext'
using FastPoint.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models; // Para o Swagger
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// 1. Configurar Conexão com Banco de Dados (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// 2. Configurar Injeção de Dependência (DI)
// Repositórios
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
// (Adicionar outros repositórios específicos aqui, se houver)

// Serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
// (Adicionar outros serviços aqui)

// 3. Adicionar Serviços de API
builder.Services.AddControllers();

// 4. Configurar Autenticação (JWT)
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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});

// 5. Configurar Autorização (Roles baseadas no seu Enum 'PerfilUsuario')
builder.Services.AddAuthorization(options =>
{
    // Requisito do BRD e Perfis
    options.AddPolicy("AdminOnly", policy => policy.RequireRole(PerfilUsuario.Administrador.ToString()));
    options.AddPolicy("GerenteOuAdmin", policy => 
        policy.RequireRole(PerfilUsuario.Administrador.ToString(), PerfilUsuario.Gerente.ToString()));
    options.AddPolicy("AtendentePolicy", policy => 
        policy.RequireRole(PerfilUsuario.Administrador.ToString(), PerfilUsuario.Gerente.ToString(), PerfilUsuario.Atendente.ToString()));
});


// 6. Configurar Swagger (Requisito do professor)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "FastPoint API", Version = "v1" });

    // Configuração para o Swagger usar o token JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, insira 'Bearer' seguido de um espaço e o token JWT",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// --- Construção do App (Configuração do Pipeline HTTP) ---
var app = builder.Build();

// 7. Configurar Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FastPoint API v1"));
}

// 8. Adicionar o Middleware de Erro Global (Precisa vir antes da autorização)
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

// 9. Habilitar Autenticação e Autorização
app.UseAuthentication(); // <-- Primeiro Autenticação
app.UseAuthorization();  // <-- Depois Autorização

app.MapControllers();

app.Run();

internal class UsuarioService
{
}

internal interface IUsuarioService
{
}

internal class ProdutoService
{
}

internal interface IProdutoService
{
}

internal class GlobalExceptionHandlerMiddleware
{
}