public interface ISimulationUpdateService : IService
{
    void Register(ITickable tickable);
    void Unregister(ITickable tickable);
    void Tick();
}