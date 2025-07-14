public class FiringState : ITowerState
{
    private readonly TowerModel _model;

    public FiringState(TowerModel model)
    {
        _model = model;
    }

    public void Enter()
    {
        _model.Shoot();
        _model.StateMachine.ChangeState<CooldownState>();
    }

    public void Tick() { }

    public void Exit() { }
}