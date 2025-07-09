using UnityEngine;

public class CooldownState : ITowerState
{
    private readonly TowerModel _towerModel;
    private readonly TowerStateMachine _stateMachine;
    private float _currentCooldown;

    public CooldownState(TowerModel towerModel, TowerStateMachine stateMachine)
    {
        _towerModel = towerModel;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _currentCooldown = _towerModel.FireCooldown;
    }

    public void Exit() { }

    public void Tick()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown <= 0f)
            _stateMachine.ChangeState<IdleState>();
    }
}
