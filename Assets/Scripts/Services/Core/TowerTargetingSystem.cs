using UnityEngine;

public class TowerTargetingSystem : ITowerTargetingSystem
{
    public ITarget FindTarget(TowerModel model, DetectedArea area)
    {
        return area.GetFirstEnemy();
    }

    public Vector3 CalculateStraightFireVelocityVector(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed)
    {
        Vector3 toTarget = targetPos - shooterPos;
        float distance = toTarget.magnitude;

        float timeToTarget = distance / projectileSpeed;
        Vector3 leadOffset = targetPos + targetVelocity * timeToTarget;

        return (leadOffset - shooterPos).normalized;
    }

    public Vector3 CalculateStraightAimOffsetVector(Vector3 gunPosition, Vector3 enemyCurrentPosition, Vector3 enemyVelocity, float aimDuration, float projectileSpeed)
    {
        Vector3 offsetDuringRotation = enemyVelocity * aimDuration;
        Vector3 predictedTargetPos = enemyCurrentPosition + offsetDuringRotation;
        Vector3 toFutureTarget = predictedTargetPos - gunPosition;

        float distance = toFutureTarget.magnitude;
        float timeToImpact = distance / projectileSpeed;
        
        Vector3 futureOffset = enemyVelocity * timeToImpact;
        Vector3 aimPoint = predictedTargetPos + futureOffset;
        return aimPoint - gunPosition;
    }

    public bool CalculateParabolicAimOffsetVector(Vector3 gunPosition, Vector3 targetPosition, Vector3 targetVelocity, float aimDuration, float projectileSpeed, out Vector3 initialVelocity)
    {
        Vector3 predictedTargetPos = targetPosition + targetVelocity * aimDuration;
        return SolveBallisticArc(gunPosition, predictedTargetPos, targetVelocity, projectileSpeed, out initialVelocity);
    }

    public bool CalculateParabolicVelocity(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed, out Vector3 initialVelocity)
    {
        return SolveBallisticArc(shooterPos, targetPos, targetVelocity, projectileSpeed, out initialVelocity);
    }

    private static bool SolveBallisticArc(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed, out Vector3 initialVelocity)
    {

        initialVelocity = Vector3.zero;
        float gravity = -Physics.gravity.y;

        const int maxIterations = 100;
        const float timeStep = 0.1f;

        for (int i = 1; i <= maxIterations; i++)
        {
            float t = i * timeStep;

            Vector3 futureTargetPos = targetPos + targetVelocity * t;
            Vector3 toTarget = futureTargetPos - shooterPos;
            Vector3 toTargetXZ = new Vector3(toTarget.x, 0, toTarget.z);

            float horizontalDist = toTargetXZ.magnitude;
            float verticalDist = toTarget.y;
            float speedSquared = projectileSpeed * projectileSpeed;
            float requiredVerticalSpeed = (verticalDist + 0.5f * gravity * t * t) / t;
            float requiredHorizontalSpeed = horizontalDist / t;

            if (requiredVerticalSpeed * requiredVerticalSpeed + requiredHorizontalSpeed * requiredHorizontalSpeed <= speedSquared)
            {
                Vector3 horizontalDir = toTargetXZ.normalized;
                Vector3 velocity = horizontalDir * requiredHorizontalSpeed;
                velocity.y = requiredVerticalSpeed;

                initialVelocity = velocity;
                return true;
            }
        }

        return false;
    }
}
