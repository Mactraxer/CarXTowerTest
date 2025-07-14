using System;
using System.Collections;
using UnityEngine;

public class CannonTowerView : TowerView
{
    [SerializeField] private Transform horizontalPivot;
    [SerializeField] private Transform verticalPivot;

    public Vector3 GunPivotPosition => verticalPivot.position;

    private Coroutine _rotateCoroutine;

    public void AimAt(Vector3 aimDirection, float aimTime, Action callback)
    {
        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(RotateTowards(aimDirection, aimTime, callback));
    }

    private IEnumerator RotateTowards(Vector3 targetDirection, float duration, Action callback)
    {
        // --- Горизонтальный поворот
        Vector3 horizontalDirection = new Vector3(targetDirection.x, 0f, targetDirection.z);
        if (horizontalDirection.sqrMagnitude < 0.001f)
        {
            horizontalDirection = horizontalPivot.forward;
        }

        Quaternion initialBaseRot = horizontalPivot.rotation;
        Quaternion targetBaseRot = Quaternion.LookRotation(horizontalDirection);

        // --- Вертикальный поворот
        Vector3 gunLocalDirection = verticalPivot.parent.InverseTransformDirection(targetDirection.normalized);
        float pitchAngle = -Mathf.Atan2(gunLocalDirection.y, gunLocalDirection.z) * Mathf.Rad2Deg;
        Quaternion initialGunRot = verticalPivot.localRotation;
        Quaternion targetGunRot = Quaternion.Euler(pitchAngle, 0f, 0f);

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            horizontalPivot.rotation = Quaternion.Slerp(initialBaseRot, targetBaseRot, t);
            verticalPivot.localRotation = Quaternion.Slerp(initialGunRot, targetGunRot, t);

            yield return null;
        }

        horizontalPivot.rotation = targetBaseRot;
        verticalPivot.localRotation = targetGunRot;
        callback?.Invoke();
    }
}