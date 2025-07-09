public class FiringState : ITowerState
{
    private readonly TowerModel _model;
    private readonly TowerStateMachine _stateMachine;

    public FiringState(TowerModel model, TowerStateMachine stateMachine)
    {
        _model = model;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _model.Shoot(); // Сигнал на выстрел
        _stateMachine.ChangeState<CooldownState>();
    }

    public void Tick() { }

    public void Exit() { }
}
