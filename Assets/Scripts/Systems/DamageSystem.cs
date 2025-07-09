public class DamageSystem : IDamageSystem
{
    public void ApplyDamage(ITarget target, float amount)
    {
        target.TakeDamage(amount);
    }
}
