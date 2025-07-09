using UnityEngine;

public class GuidedProjectileModel : ProjectileModel
{
    public GuidedProjectileModel(Vector3 startPosition, float speed, float damage, ITarget target) : base(startPosition, speed, damage, target)
    {
    }
}
