using UnityEngine;

public interface ITowerTargetingSystem : IService
{
    ITarget FindTarget(TowerModel model, DetectedArea area);
    Vector3 CalculateStraightFireVelocityVector(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed);
    Vector3 CalculateStraightAimOffsetVector(Vector3 position, Vector3 currentPosition, Vector3 velocity, float aimDuration, float projectileSpeed);
    bool CalculateParabolicVelocity(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed, out Vector3 initialVelocity);
    bool CalculateParabolicAimOffsetVector(Vector3 gunPosition, Vector3 targetPosition, Vector3 targetVelocity, float aimDuration, float projectileSpeed, out Vector3 initialVelocity);
}