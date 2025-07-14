public class SearchTargetState : ITowerState
{
    private readonly TowerModel _model;

    public SearchTargetState(TowerModel towerModel)
    {
        _model = towerModel;
    }

    public void Enter()
    {
        var target = _model.TargetingSystem.FindTarget(_model, _model.DetectedArea);
        _model.SetTarget(target);
        _model.DetectedTaget();
    }

    public void Exit() { }

    public void Tick() { }
}