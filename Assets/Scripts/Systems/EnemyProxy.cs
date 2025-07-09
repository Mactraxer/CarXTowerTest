using UnityEngine;

public class EnemyProxy : MonoBehaviour
{
    public IEnemy Enemy { get; private set; }

    public void Init(IEnemy enemy)
    {
        Enemy = enemy;
    }
}
