public interface IDamageSystem : IService
{
    public void ApplyDamage(ITarget target, ProjectileModel projectileModel);
}