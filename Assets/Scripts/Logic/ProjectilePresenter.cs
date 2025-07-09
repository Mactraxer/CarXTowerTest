using System;

public class ProjectilePresenter : IDisposable, ITickable
{
    private ProjectileModel _model;
    private ProjectileView _view;

    public ProjectilePresenter(ProjectileModel model, ProjectileView view)
    {
        _model = model;
        _view = view;
    }

    public void Tick()
    {
        _model.Tick();
        _view.SetPosition(_model.Position);
    }

    public void Dispose()
    {
        
    }
}
