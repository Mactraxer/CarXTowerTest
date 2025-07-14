using UnityEngine;

public class TowerView : MonoBehaviour
{
    [SerializeField] protected Transform shootPoint;

    public Vector3 ShootPointPosition => shootPoint.position;

    public virtual void Shoot()
    {
        // VFX, Sound, etc.
    }
}
