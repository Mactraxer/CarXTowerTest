using System;

public class EnemyPresenter : IDisposable, ITickable
{
    public event Action<EnemyPresenter> OnDied;
    public event Action<EnemyPresenter> OnReachDestination;

    private readonly EnemyModel model;
    private readonly EnemyView view;

    public EnemyPresenter(EnemyModel model, EnemyView view)
    {
        this.model = model;
        this.view = view;
        Init();
    }

    public void Init()
    {
        model.OnReachedDestination += OnReachedDestinationHandler;
        model.OnTakeDamage += OnTakeDamageHandler;
        model.OnDeath += OnDeathHandler;
        model.Init();
        view.UpdateHealthState(model.Health / model.MaxHealht);
        view.gameObject.SetActive(true);
    }

    public void Dispose()
    {
        model.OnReachedDestination -= OnReachedDestinationHandler;
        model.OnTakeDamage -= OnTakeDamageHandler;
        model.OnDeath -= OnDeathHandler;
        model.Dispose();
        view.gameObject.SetActive(false);
    }

    public void Tick()
    {
        model.Tick();
        view.UpdatePosition(model.CurrentPosition);
    }

    public void StartMove()
    {
        model.StartMove();
        view.UpdatePosition(model.CurrentPosition);
    }

    private void OnTakeDamageHandler()
    {
        view.UpdateHealthState(model.Health / model.MaxHealht);
    }

    private void OnReachedDestinationHandler(EnemyModel model)
    {
        OnReachDestination?.Invoke(this);
    }
    
    private void OnDeathHandler(IEnemy _)
    {
        OnDied?.Invoke(this);
    }
}
