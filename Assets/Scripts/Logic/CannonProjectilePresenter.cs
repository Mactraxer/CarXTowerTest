public class CannonProjectilePresenter : ProjectilePresenter
{
    private CannonProjectileView cannonView;
    private CannonProjectileModel cannonModel;

    public CannonProjectilePresenter(CannonProjectileView cannonView, CannonProjectileModel cannonModel) : base(cannonModel, cannonView)
    {
        this.cannonView = cannonView;
        this.cannonModel = cannonModel;
    }
}
