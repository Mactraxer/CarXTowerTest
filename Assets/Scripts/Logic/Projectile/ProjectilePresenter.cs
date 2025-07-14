using System;
using UnityEngine;

public abstract class ProjectilePresenter : IDisposable, ITickable
{
    public Action<ProjectilePresenter> OnLifeTimeEnded;
    public ProjectileModel Model => _model;
    private ProjectileModel _model;
    private ProjectileView _view;
    protected Action<ProjectilePresenter> _onHitCallback;
    protected readonly float _lifeTime;
    protected float _currentLifeTime;

    public ProjectilePresenter(ProjectileModel model, ProjectileView view, float lifeTime)
    {
        _model = model;
        _view = view;
        Init(model.Position);
        _lifeTime = lifeTime;
    }

    public void Init(Vector3 startPosition)
    {
        _view.gameObject.SetActive(true);
        _model.SetPosition(startPosition);
        _view.SetPosition(startPosition);
    }

    public void Tick()
    {
        _model.Tick();
        _view.SetPosition(_model.Position);

        if (_model.IsActive)
        {
            _currentLifeTime -= Time.deltaTime;
            if (_currentLifeTime <= 0)
            {
                OnLifeTimeEnded?.Invoke(this);
            }
        }
    }

    public void Dispose()
    {
        _model.Dispose();
        _view.gameObject.SetActive(false);
    }

    protected void OnHitCallbackHandler(ProjectileModel _)
    {
        _onHitCallback?.Invoke(this);
    }
}
