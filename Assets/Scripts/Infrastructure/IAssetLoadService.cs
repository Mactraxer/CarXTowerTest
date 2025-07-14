public interface IAssetLoadService : IService
{
    T Load<T>(string path) where T : UnityEngine.Object;
    T Load<T>(AssetReference<T> reference) where T : UnityEngine.Object;
}