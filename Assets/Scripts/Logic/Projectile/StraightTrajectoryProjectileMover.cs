using UnityEngine;

public class StraightTrajectoryProjectileMover : ProjectileMoverBase
{
    public override void MoveTo()
    {
        var newPosition = _projectileModel.Position + _speed * Time.deltaTime * _moveVelocity;
        _projectileModel.SetPosition(newPosition);
        TryInvokeHitEvent();
    }
}
