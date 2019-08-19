using Microliu.Auth.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class ServiceProviderServiceExtensions
{
    public static T GetServices<T>(this IServiceProvider provider, DbType dbType)
    {
        var services = provider.GetServices<T>();
        return services.Where(r => (DbType)r.GetType().GetMethod("GetDbType").Invoke(r, null) == dbType).FirstOrDefault();
    }
}
