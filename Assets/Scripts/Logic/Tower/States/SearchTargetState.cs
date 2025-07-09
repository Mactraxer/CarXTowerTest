public class SearchTargetState : ITowerState
{
    private TowerModel _towerModel;
    private TowerStateMachine _stateMachine;

    public SearchTargetState(TowerModel towerModel, TowerStateMachine stateMachine)
    {
        _towerModel = towerModel;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        var target = _towerModel.TargetingSystem.FindTarget(_towerModel);
        _towerModel.SetTarget(target);
        _stateMachine.ChangeState<AimingState>();
    }

    public void Exit()
    {
    }

    public void Tick()
    {
    }
}