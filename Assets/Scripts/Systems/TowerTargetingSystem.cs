using System;
using UnityEngine;

public class TowerTargetingSystem
{
    public ITarget FindTarget(TowerModel model)
    {
        return null;
    }

    public Vector3 CalculateLeadVector(
        Vector3 shooterPos,
        Vector3 targetPos,
        Vector3 targetVelocity,
        float projectileSpeed)
    {
        Vector3 toTarget = targetPos - shooterPos;
        float distance = toTarget.magnitude;

        // Примерная оценка времени полёта до цели
        float timeToTarget = distance / projectileSpeed;

        // Предсказанное смещение врага за это время
        Vector3 leadOffset = targetVelocity * timeToTarget;

        return leadOffset;
    }
}