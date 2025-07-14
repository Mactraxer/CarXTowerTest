using UnityEngine;

public class CooldownState : ITowerState
{
    private readonly TowerModel _model;

    private float _currentCooldown;

    public CooldownState(TowerModel towerModel)
    {
        _model = towerModel;
    }

    public void Enter()
    {
        _currentCooldown = _model.FireCooldown;
    }

    public void Tick()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown <= 0f)
            _model.StateMachine.ChangeState<IdleState>();
    }

    public void Exit() { }
}
