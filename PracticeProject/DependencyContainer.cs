using PracticeProject.Services;
using PracticeProject.Services.Interfaces;

namespace PracticeProject
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IPracticeProj, PracticeProjService>();
        }
    }
}
