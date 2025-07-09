using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
public class LevelConfigSO : ScriptableObject
{
    public TowerSlot[] towerSlots;
    public Vector3 enemySpawnPoint;
    public Vector3 enemyDestinationPoint;
    public float spawnEnemyDelay = 2f;
    public AssetReference<LevelView> levelViewPrefab;
#if UNITY_EDITOR
    private void OnValidate()
    {
        levelViewPrefab.OnValidate();
    }
#endif
}