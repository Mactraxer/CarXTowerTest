using UnityEngine;

public class TowerTargetingSystem : ITowerTargetingSystem
{
    public ITarget FindTarget(TowerModel model, DetectedArea area)
    {
        return area.GetFirstEnemy();
    }

    public bool CalculateStraightFireDirectionVector(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed, out Vector3 fireDirection)
    {
        fireDirection = Vector3.zero;

        Vector3 toTarget = targetPos - shooterPos;
        if (!SolveAnalyticStraightTrajectory(toTarget, targetVelocity, projectileSpeed, out float t))
        {
            return false; // Нет реального пересечения
        }

        Vector3 aimPoint = targetPos + targetVelocity * t;
        fireDirection = (aimPoint - shooterPos).normalized;
        return true;
    }

    public bool CalculateStraightAimDirectionVector(Vector3 gunPosition, Vector3 targetPosition, Vector3 targetVelocity, float aimDuration, float projectileSpeed, out Vector3 aimDirection)
    {
        aimDirection = Vector3.zero;

        Vector3 predictedTargetPos = targetPosition + targetVelocity * aimDuration;
        Vector3 toTarget = predictedTargetPos - gunPosition;
        
        if (!SolveAnalyticStraightTrajectory(toTarget, targetVelocity, projectileSpeed, out float t))
        {
            return false; // Нет реального пересечения
        }

        Vector3 aimPoint = predictedTargetPos + targetVelocity * t;
        aimDirection = (aimPoint - gunPosition).normalized;

        return true;
    }

    public bool CalculateParabolicAimDirectionVector(Vector3 gunPosition, Vector3 targetPosition, Vector3 targetVelocity, float aimDuration, float projectileSpeed, out Vector3 initialVelocity)
    {
        Vector3 predictedTargetPos = targetPosition + targetVelocity * aimDuration;
        return SolveBallisticArc(gunPosition, predictedTargetPos, targetVelocity, projectileSpeed, out initialVelocity);
    }

    public bool CalculateParabolicFireVelocity(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float projectileSpeed, out Vector3 initialVelocity)
    {
        return SolveBallisticArc(shooterPos, targetPos, targetVelocity, projectileSpeed, out initialVelocity);
    }

    private bool SolveAnalyticStraightTrajectory(Vector3 toTarget, Vector3 targetVelocity, float projectileSpeed, out float time)
    {
        time = 0f;
        float distanceSquared = toTarget.sqrMagnitude;

        float targetSpeedSquared = targetVelocity.sqrMagnitude;
        float projectileSpeedSquared = projectileSpeed * projectileSpeed;

        float a = targetSpeedSquared - projectileSpeedSquared;
        float b = 2f * Vector3.Dot(toTarget, targetVelocity);
        float c = distanceSquared;

        float discriminant = b * b - 4f * a * c;

        if (discriminant < 0f)
            return false; // Нет реального пересечения

        float sqrtDiscriminant = Mathf.Sqrt(discriminant);

        if (Mathf.Abs(a) < 0.001f)
        {
            // Почти нулевая разность скоростей, линейное уравнение
            time = -c / b;
        }
        else
        {
            float t1 = (-b - sqrtDiscriminant) / (2f * a);
            float t2 = (-b + sqrtDiscriminant) / (2f * a);

            // Берем минимальное положительное время
            time = Mathf.Min(t1, t2);
            if (time < 0f) time = Mathf.Max(t1, t2);
            if (time < 0f) return false; // Обе t отрицательные — цель уже ушла
        }

        return true; // Успешно нашли время
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
