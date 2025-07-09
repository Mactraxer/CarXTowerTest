using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "Configs/Tower")]
public class TowerConfigSO : ScriptableObject
{
    public TowerType type;
    public ProjectileConfigSO projectileConfig;
    public float fireRate = 1f;
    public float range = 10f;

    public AssetReference<GameObject> towerPrefab;

#if UNITY_EDITOR
    private void OnValidate()
    {
        towerPrefab.OnValidate();
    }
#endif
}
