using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;
using Hospital.Application.Common;
using Hospital.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Hospital.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHospitalApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        services.AddHospitalCors(configuration);
        services.AddHospitalAuthentication(configuration);
        services.AddHospitalAuthorization();
        services.AddEndpointsApiExplorer();
        services.AddHospitalSwagger();

        return services;
    }

    private static void AddHospitalCors(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCors", policy =>
            {
                if (origins.Length > 0)
                {
                    policy
                        .WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
                else
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }
            });
        });
    }

    private static void AddHospitalAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var jwtKey = configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "Jwt:Key no configurada. Usa User Secrets o variables de entorno.");

        if (jwtKey.Length < 32)
            throw new InvalidOperationException("Jwt:Key debe tener al menos 32 caracteres.");

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.MapInboundClaims = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                RoleClaimType = "role",
                NameClaimType = "unique_name"
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    if (context.Response.HasStarted)
                        return;

                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var message = context.AuthenticateFailure switch
                    {
                        SecurityTokenExpiredException =>
                            "Token expirado. Usa POST /api/auth/refresh o vuelve a iniciar sesión.",
                        _ => "No autorizado. Envía el header Authorization: Bearer {token}."
                    };

                    var payload = ApiResponseFactory.Fail(message);
                    await context.Response.WriteAsync(JsonSerializer.Serialize(payload, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    }));
                }
            };
        });
    }

    private static void AddHospitalAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.AdminOnly, policy =>
                policy.RequireRole(AppRoles.Admin));

            options.AddPolicy(AuthorizationPolicies.Staff, policy =>
                policy.RequireRole(AppRoles.Admin, AppRoles.Medico, AppRoles.Recepcion));
        });
    }

    private static void AddHospitalSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT: Bearer {token}"
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
                    Array.Empty<string>()
                }
            });
        });
    }
}
