using System;
using UnityEngine;

public class TowerPresenter : IDisposable, ITickable
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

    private void OnStartAim(ITarget target)
    {
        view.AimAt(target.CurrentPosition, model.RotateDuration, () =>
        {
            model.StateMachine.ChangeState<FiringState>();
        });
    }

    public void Tick()
    {
        model.Tick();
    }

    public void Dispose()
    {
        model.OnAimAtTarget -= OnStartAim;
        model.OnShoot -= OnStartShoot;
    }
}
