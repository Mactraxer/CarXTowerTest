using System;

public interface ISceneLoader : IService
{
    public void LoadScene(string name, Action onLoaded = null);
}