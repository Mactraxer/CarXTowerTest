using UnityEngine;

public class CannonProjectileModel : ProjectileModel
{
    public CannonProjectileModel(Vector3 startPosition, float speed, float damage, ITarget target) : base(startPosition, speed, damage, target)
    {
    }
}
