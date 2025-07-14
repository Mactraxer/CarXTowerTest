interface IServiceLocator
{
    void Register<TService>(TService service) where TService : IService;
    TService Resolve<TService>() where TService : class, IService;
}
