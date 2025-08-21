using CarKilometerTrack.Interfaces;
using CarKilometerTrack.Services;

namespace CarKilometerTrack.Helpers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ICarServices, CarServices>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<ILogServices, LogServices>();
            services.AddScoped<INotesServices, NotesServices>();
            services.AddScoped<IReportService, ReportService>();
            return services;
        }
    }
}
