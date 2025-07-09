using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    public void LoadScene(string name, Action onLoaded = null)
    {
        if (name == SceneManager.GetActiveScene().name)
        {
            onLoaded?.Invoke();
            return;
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(name);
        loadOperation.completed += _ => onLoaded?.Invoke();
    }
}
