public class GuidedProjectilePresenter : ProjectilePresenter
{
    private GuidedProjectileView guidedView;
    private GuidedProjectileModel guidedModel;

    public GuidedProjectilePresenter(GuidedProjectileView guidedView, GuidedProjectileModel guidedModel) : base(guidedModel, guidedView)
    {
        this.guidedView = guidedView;
        this.guidedModel = guidedModel;
    }
}
