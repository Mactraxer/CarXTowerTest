using System;
using System.Collections.Generic;

class ServiceLocator : IServiceLocator
{
    private readonly Dictionary<Type, IService> services = new();

    public void Register<TService>(TService service) where TService : IService
    {
        var type = typeof(TService);
        if (services.ContainsKey(type))
            throw new Exception($"Service of type {type.Name} already registered.");

        services.Add(type, service);
    }

    public TService Resolve<TService>() where TService : class, IService
    {
        var type = typeof(TService);
        if (services.TryGetValue(type, out var service))
            return service as TService;

        throw new Exception($"Service of type {type.Name} not registered.");
    }
}