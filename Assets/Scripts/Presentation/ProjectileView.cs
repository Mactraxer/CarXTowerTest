using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    public Transform visualTransform;

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
