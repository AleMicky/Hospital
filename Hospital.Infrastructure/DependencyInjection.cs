using Hospital.Application.Interfaces;
using Hospital.Infrastructure.Data;
using Hospital.Infrastructure.Identity;
using Hospital.Infrastructure.Services;
using Hospital.Infrastructure.Services.Catalogos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hospital.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddIdentity<ApplicationUser, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<SeedOptions>(configuration.GetSection(SeedOptions.SectionName));
        services.AddScoped<IDataSeeder, DataSeeder>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        
        services.AddScoped<IPacienteService, PacienteService>();
        services.AddScoped<ICatalogoGrupoService, CatalogoGrupoService>();
        services.AddScoped<ICatalogoItemService, CatalogoItemService>();
        services.AddScoped<ITipoAtencionService, TipoAtencionService>();
        services.AddScoped<IFormularioService, FormularioService>();
        services.AddScoped<IFormularioCampoService, FormularioCampoService>();
        services.AddScoped<IAtencionService, AtencionService>();
        services.AddScoped<IAtencionValorService, AtencionValorService>();

        services.AddScoped<IAreaService, AreaService>();
        services.AddScoped<IDepartamentoService, DepartamentoService>();
        services.AddScoped<IServicioService, ServicioService>();
        services.AddScoped<IPrestacionService, PrestacionService>();
        services.AddScoped<IEspecialidadService, EspecialidadService>();
        services.AddScoped<IProfesionService, ProfesionService>();
        services.AddScoped<ICargoService, CargoService>();
        services.AddScoped<ITipoAtencionCatalogoService, TipoAtencionCatalogoService>();
        
        return services;
    }
}