using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using VPToDoTask.Application.Behaviours;
using VPToDoTask.Application.Helpers;
using VPToDoTask.Application.Interfaces;
using VPToDoTask.Domain.Entities;

namespace VPToDoTask.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Register FluentValidation validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            // Register MediatR pipeline behavior
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            
            services.AddScoped<IDataShapeHelper<Todo>, DataShapeHelper<Todo>>();
            services.AddScoped<IModelHelper, ModelHelper>();
        }
    }
}