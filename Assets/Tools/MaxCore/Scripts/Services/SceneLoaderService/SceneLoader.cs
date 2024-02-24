using System;
using UnityEngine.SceneManagement;

namespace Tools.MaxCore.Scripts.Services.SceneLoaderService
{
    public class SceneLoader
    {
        public Scene LoadScene(string scene, LoadSceneMode mode = LoadSceneMode.Single, Action onSuccess = null)
        {
            SceneManager.LoadScene(scene, mode);
            
            onSuccess?.Invoke();

            return SceneManager.GetSceneByName(scene);
        }
    }
}