using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VPToDoTask.Application.Interfaces;
using VPToDoTask.Infrastructure.Shared.Services;
using VPToDoTask.Domain.Settings;

namespace VPToDoTask.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMockService, MockService>();
        }
    }
}