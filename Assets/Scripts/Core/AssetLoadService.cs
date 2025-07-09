using System;
using UnityEngine;

public class AssetLoadService : IAssetLoadService
{
    private const string ResourcePath = "Prefabs/";

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        var asset = Resources.Load<T>(path);
        if (!asset)
            throw new Exception($"Asset at path '{path}' not found.");
        return asset;
    }

    public T Load<T>(AssetReference<T> reference) where T : UnityEngine.Object
    {
        return Load<T>(reference.Path);
    }
}
