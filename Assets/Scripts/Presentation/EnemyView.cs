using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }
}
