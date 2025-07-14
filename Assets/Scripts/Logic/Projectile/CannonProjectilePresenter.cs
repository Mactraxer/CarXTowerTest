using System;
using UnityEngine;

public class CannonProjectilePresenter : ProjectilePresenter
{
    private CannonProjectileView cannonView;
    private CannonProjectileModel cannonModel;

    public CannonProjectilePresenter(CannonProjectileModel cannonModel, CannonProjectileView cannonView, float lifeTime) : base(cannonModel, cannonView, lifeTime)
    {
        this.cannonView = cannonView;
        this.cannonModel = cannonModel;
    }

    public void MoveToTargetWithOffset(ITarget target, Vector3 aimPoint, Action<ProjectilePresenter> onHitCallback)
    {
        _currentLifeTime = _lifeTime;
        _onHitCallback = onHitCallback;
        cannonModel.Init(target, aimPoint, OnHitCallbackHandler);
    }
}
