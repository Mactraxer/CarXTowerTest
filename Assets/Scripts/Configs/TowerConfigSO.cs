using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "Configs/Tower")]
public class TowerConfigSO : ScriptableObject
{
    public TowerType type;
    public ProjectileConfigSO projectileConfig;
    public float fireCooldonw = 1f;
    public float range = 10f;

    public AssetReference<GameObject> towerPrefab;
    public TrajectoryMode trajectoryMode;

#if UNITY_EDITOR
    private void OnValidate()
    {
        towerPrefab.OnValidate();
        if (type == TowerType.Cannon && projectileConfig.trajectoryMode != trajectoryMode)
        {
            Debug.LogError($"Trajectory mode in {name} does not match projectile config trajectory mode. Tower type: {type}, Trajectory mode: {trajectoryMode}, Projectile trajectory mode: {projectileConfig.trajectoryMode}", this);
            trajectoryMode = projectileConfig.trajectoryMode;
        }
    }
#endif
}
