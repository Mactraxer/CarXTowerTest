using System;
using UnityEngine;

[Serializable]
public class AssetReference<T> where T : UnityEngine.Object
{
    [SerializeField] private T asset;
    [SerializeField, HideInInspector] private string path;

    public string Path => path;
    public T Asset => asset;

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (asset != null)
        {
            path = UnityEditor.AssetDatabase.GetAssetPath(asset);
            var resourcesIndex = path.IndexOf("Resources/");
            if (resourcesIndex >= 0)
            {
                path = path.Substring(resourcesIndex + "Resources/".Length);
                path = System.IO.Path.ChangeExtension(path, null);
            }
        }
    }
#endif
}