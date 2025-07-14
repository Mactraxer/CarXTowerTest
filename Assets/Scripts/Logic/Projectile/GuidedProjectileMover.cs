using UnityEngine;

class GuidedProjectileMover : ProjectileMoverBase
{
    public override void MoveTo()
    {
        var direction = (_target.CurrentPosition - _projectileModel.Position).normalized;

        var newPosition = _projectileModel.Position + direction * _speed * Time.deltaTime;
        _projectileModel.SetPosition(newPosition);
        TryInvokeHitEvent();
    }
}
