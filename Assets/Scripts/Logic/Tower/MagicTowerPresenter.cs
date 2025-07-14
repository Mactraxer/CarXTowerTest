public class MagicTowerPresenter : TowerPresenter
{
    private MagicTowerModel _magicModel;
    private MagicTowerView _magicView;

    public MagicTowerPresenter(MagicTowerModel model, MagicTowerView view) : base(model, view)
    {
        _magicModel = model;
        _magicView = view;
    }

    protected override void OnStartAim(ITarget target)
    {
        target.OnDisposed += OnTagetDisposedHandler;
        _magicModel.OnStopAimTarget += OnStopAimTargetHandler;
    }

    private void OnStopAimTargetHandler()
    {
        _magicModel.Target.OnDisposed -= OnTagetDisposedHandler;
    }

    private void OnTagetDisposedHandler()
    {
        _magicModel.Target.OnDisposed -= OnTagetDisposedHandler;
        _magicModel.StateMachine.ChangeState<SearchTargetState>();
    }
}
