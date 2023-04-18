using System;
using System.Collections;
using System.Collections.Generic;
using Attributes;
using Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Service
{
public class SceneService : ISceneService
{
    //  [ServiceInit]
    public void LoadScene(Action<AsyncOperation> callback)
    {

        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single).completed += callback;
    }

    public void LoadScene(string sceneName, Action<AsyncOperation> callback)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).completed += callback;
    }
}
}
