public interface IEnemyFactory : IService
{
    public EnemyPresenter Create();
    void Dispose(EnemyPresenter presenter);
}