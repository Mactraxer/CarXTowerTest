using System;

public class GuidedProjectilePresenter : ProjectilePresenter
{
    private GuidedProjectileView _guidedView;
    private GuidedProjectileModel _guidedModel;

    public GuidedProjectilePresenter(GuidedProjectileModel guidedModel, GuidedProjectileView guidedView, float lifeTime) : base(guidedModel, guidedView, lifeTime)
    {
        _guidedView = guidedView;
        _guidedModel = guidedModel;
    }

    public void MoveToTarget(ITarget target, Action<ProjectilePresenter> onHitCallback)
    {
        _currentLifeTime = _lifeTime;
        _onHitCallback = onHitCallback;
        _guidedModel.Init(target, OnHitCallbackHandler);
    }
}
