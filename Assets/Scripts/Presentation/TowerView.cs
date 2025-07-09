using System;
using System.Collections;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    [SerializeField] private Transform gunPivot;
    [SerializeField] private Transform shootPoint;
    private Coroutine _rotateCoroutine;

    public void Shoot()
    {
        // VFX, Sound, etc.
    }

    public void AimAt(Vector3 aimDirection, float aimTime, Action callback)
    {
        if (_rotateCoroutine != null)
            StopCoroutine(_rotateCoroutine);

        _rotateCoroutine = StartCoroutine(RotateTowards(aimDirection, aimTime, callback));
    }

    private IEnumerator RotateTowards(Vector3 targetDirection, float duration, Action callback)
    {
        Quaternion initialRotation = gunPivot.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            gunPivot.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        gunPivot.rotation = targetRotation;
        callback?.Invoke();
    }
}
