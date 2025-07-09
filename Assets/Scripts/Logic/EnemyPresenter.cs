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
        model.OnReachedDestination += OnReachedDestination;
        model.OnDeath += OnDeath;
        view.gameObject.SetActive(true);
    }

    public void Dispose()
    {
        model.OnReachedDestination -= OnReachedDestination;
        model.OnDeath -= OnDeath;
        view.gameObject.SetActive(false);
    }

    public void ApplyDamage(float amount)
    {
        model.TakeDamage(amount);
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

    private void OnReachedDestination(EnemyModel model)
    {
        OnReachDestination?.Invoke(this);
    }
    
    private void OnDeath(IEnemy _)
    {
        OnDied?.Invoke(this);
    }
}
