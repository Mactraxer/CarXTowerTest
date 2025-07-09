public class AimingState : ITowerState
{
    private readonly TowerModel _model;
    private readonly TowerStateMachine _stateMachine;

    public AimingState(TowerModel model, TowerStateMachine stateMachine)
    {
        _model = model;
        _stateMachine = stateMachine;
    }

    public void Enter() { }

    public void Tick()
    {
        var leadOffset = _model.TargetingSystem.CalculateLeadVector(_model.Position, _model.Target.CurrentPosition, _model.Target.Velocity, _model.ProjectileSpeed);
        _model.SetLeadOffset(leadOffset);
        _model.Aim(); // Сигнал на вращение башни

    }

    public void Exit() { }
}
