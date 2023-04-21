using UnityEngine;

namespace Service
{
    public interface IPoolService : IService
    {
        public void CreatePool(GameObject gameObject, int count);
        public void RemovePool(GameObject gameObject);
        public GameObject GetFromPool(GameObject gameObject);
        public void AddToPool(GameObject gameObject);
        public void AddToPoolLater(GameObject gameObjectReference, GameObject gameObjectItem, float lifeTime);
    }
}