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
    public void LoadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
}
