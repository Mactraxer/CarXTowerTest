using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private Color maxHealthColor = Color.green;
    private Color minHealthColor = Color.red;

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
    }

    public void UpdateHealthState(float healthPercent)
    {
        meshRenderer.material.color = Color.Lerp(minHealthColor, maxHealthColor, healthPercent);
    }
}
