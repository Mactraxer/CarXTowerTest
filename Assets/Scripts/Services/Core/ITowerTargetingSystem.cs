using UnityEngine;

public interface ITowerTargetingSystem : IService
{
    ITarget FindTarget(TowerModel model, DetectedArea area);
    bool CalculateStraightFireDirectionVector(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed, out Vector3 fireDirection);
    bool CalculateStraightAimDirectionVector(Vector3 gunPosition, Vector3 targetPosition, Vector3 targetVelocity, float aimDuration, float projectileSpeed, out Vector3 aimDirection);
    bool CalculateParabolicFireVelocity(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed, out Vector3 initialVelocity);
    bool CalculateParabolicAimDirectionVector(Vector3 gunPosition, Vector3 targetPosition, Vector3 targetVelocity, float aimDuration, float projectileSpeed, out Vector3 aimDirection);
}