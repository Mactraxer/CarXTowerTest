using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EnemyConfig")]
public class EnemyConfigSO : ScriptableObject
{
    public float speed;
    public float health;
    public AssetReference<EnemyView> enemyPrefab;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        enemyPrefab.OnValidate();
    }
#endif
}
