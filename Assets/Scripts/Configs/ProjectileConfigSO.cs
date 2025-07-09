using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/Projectile")]
public class ProjectileConfigSO : ScriptableObject
{
    public ProjectileType type;
    public float speed = 10f;
    public float damage = 25f;

    public AssetReference<ProjectileView> prefab;

#if UNITY_EDITOR
    private void OnValidate()
    {
        prefab.OnValidate();
    }
#endif
}
