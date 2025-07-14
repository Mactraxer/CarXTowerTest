public class CannonTowerPresenter : TowerPresenter
{
    private readonly CannonTowerModel cannonModel;
    private readonly CannonTowerView cannonView;

    public CannonTowerPresenter(CannonTowerModel model, CannonTowerView view) : base(model, view)
    {
        cannonModel = model;
        cannonView = view;
    }

    protected override void OnStartAim(ITarget target)
    {
        target.OnDisposed += OnTargetDisposedHandler;
        cannonView.AimAt(cannonModel.AimOffset, cannonModel.AimDuration, () =>
        {
            cannonModel.Target.OnDisposed -= OnTargetDisposedHandler;
        });
    }

    private void OnTargetDisposedHandler()
    {
        cannonModel.Target.OnDisposed -= OnTargetDisposedHandler;
        cannonModel.StateMachine.ChangeState<SearchTargetState>();
    }
}
