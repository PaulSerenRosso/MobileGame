using UnityEngine;

namespace Service.Items
{
    public class PlayerItemsLinker : MonoBehaviour
    {
        [SerializeField] private Transform _hatPivot;   
        [SerializeField] private SkinnedMeshRenderer _playerTshirt; 
        [SerializeField] private SkinnedMeshRenderer _playerShort;
        
        private GameObject _currentHat;
        
        public void SetHat(GameObject prefab)
        {
            RemoveHat();
            _currentHat = Instantiate(prefab, _hatPivot);
        }

        public void SetShort(Texture sprite)
        {
            _playerShort.enabled = true;
            _playerShort.material.mainTexture = sprite;
        }

        public void SetTShirt(Texture texture)
        {
            _playerTshirt.enabled = true;
            _playerTshirt.material.mainTexture = texture;
        }

        public void RemoveHat()
        {
            if(_currentHat) Destroy(_currentHat);
        }

        public void RemoveShort()
        { 
            _playerShort.enabled = false;
        }

        public void RemoveTShirt()
        {
            _playerTshirt.enabled = false;
        }
    }
}
