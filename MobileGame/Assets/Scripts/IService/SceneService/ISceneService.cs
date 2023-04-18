using System;
using UnityEngine;


namespace Service
{
    public interface ISceneService : IService
    {
        public void LoadScene(Action<AsyncOperation> callback);
        public void LoadScene(string sceneName, Action<AsyncOperation> callback);
    }
}