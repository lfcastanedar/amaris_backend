using BLL.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public static class DependencyInjection
{
    public static void AddBusinessServices(this IHostApplicationBuilder builder)
    {
        
        builder.Services.AddScoped<IEmployeeBll, EmployeeBll>();
    }
}