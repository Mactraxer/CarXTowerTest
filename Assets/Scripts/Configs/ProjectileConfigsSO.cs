using UnityEngine;

[CreateAssetMenu(fileName = "ContainerProjectileConfigs", menuName = "Configs/ProjectileContainer")]
public class ProjectileConfigsSO : ScriptableObject
{
    public ProjectileConfigSO[] configs;
}