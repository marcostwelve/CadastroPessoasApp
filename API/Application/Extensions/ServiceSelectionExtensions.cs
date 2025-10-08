using Application.Helpers;
using Application.Mappers;
using Application.Services;
using Application.Services.Interface;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceSelectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<IUserService, UserService>();
        services.AddAutoMapper(typeof(MapperProfile).Assembly);
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        services.AddValidatorsFromAssemblyContaining<PessoaModelValidator>();
    }
}
