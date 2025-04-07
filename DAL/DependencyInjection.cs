using DAL.External;
using DAL.External.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DAL;

public static class DependencyInjection
{
    public static void AddDataAccessServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IExternalRequestDal, ExternalRequestDal>();
    }
}