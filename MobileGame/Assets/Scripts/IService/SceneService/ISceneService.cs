using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Service
{
    public interface ISceneService : IService
    {
       
        public void LoadScene();
       
       public void LoadScene(string sceneName);
    }
}