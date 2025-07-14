public class DamageSystem : IDamageSystem
{
    public void ApplyDamage(ITarget target, ProjectileModel projectileModel)
    {
        target.TakeDamage(projectileModel.Damage);
    }
}
