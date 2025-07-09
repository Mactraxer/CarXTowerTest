public class CollisionReporter : IService
{
    private readonly IDamageSystem _damageSystem;
    private readonly ProjectileFactory projectileFactory;

    public void ReportCollision(ProjectileModel projectile, ITarget enemy)
    {
        _damageSystem.ApplyDamage(enemy, projectile.Damage);
        projectileFactory.Dispose(projectile);
    }
}
