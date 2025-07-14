using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/Projectile")]
public class ProjectileConfigSO : ScriptableObject
{
    public ProjectileType type;
    public float speed = 10f;
    public float damage = 25f;

    public AssetReference<ProjectileView> prefab;
    public float lifeTime = 5f;
    public float hitDistance = 0.5f;
    public TrajectoryMode trajectoryMode;

#if UNITY_EDITOR
    private void OnValidate()
    {
        prefab.OnValidate();
    }
#endif
}
