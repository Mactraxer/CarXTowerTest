using System;
using UnityEngine;

public abstract class TowerPresenter : IDisposable, ITickable
{
    private readonly TowerModel model;
    private readonly TowerView view;

    public TowerPresenter(TowerModel model, TowerView view)
    {
        this.model = model;
        this.view = view;

        model.OnAimAtTarget += OnStartAim;
        model.OnShoot += OnStartShoot;
    }

    private void OnStartShoot(Transform transform)
    {
        view.Shoot();
    }

    protected abstract void OnStartAim(ITarget target);

    public void Tick()
    {
        model.Tick();
    }

    public void Dispose()
    {
        model.OnAimAtTarget -= OnStartAim;
        model.OnShoot -= OnStartShoot;
    }

    public void Start()
    {
        model.Start();
    }
}
