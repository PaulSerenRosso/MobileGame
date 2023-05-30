using UnityEngine;

namespace Service.Items
{
    public class PlayerItemsLinker : MonoBehaviour
    {
        [SerializeField] private Transform _hatPivot;   
        [SerializeField] private SkinnedMeshRenderer _playerTshirt; 
        [SerializeField] private SkinnedMeshRenderer _playerShort;
        [SerializeField] private MeshRenderer[] _playerSockets;
        
        private GameObject _currentHat;
        
        public void SetHat(GameObject prefab)
        {
            RemoveHat();
            _currentHat = Instantiate(prefab, _hatPivot);
        }

        public void SetTShirt(Texture sprite)
        {
            RemoveTShirt();
            _playerTshirt.enabled = true;
            _playerTshirt.material.mainTexture = sprite;
        }

        public void SetShort(Texture sprite)
        {
            RemoveShort();
            _playerShort.enabled = true;
            foreach (var playerSocket in _playerSockets)
            {
                playerSocket.enabled = true;
                playerSocket.material.mainTexture = sprite;
            }
            _playerShort.material.mainTexture = sprite;
        }

        public void RemoveHat()
        {
            if(_currentHat) Destroy(_currentHat);
        }

        public void RemoveShort()
        { 
            _playerShort.enabled = false;
            foreach (var playerSocket in _playerSockets)
            {
                playerSocket.enabled = false;
            }
        }

        public void RemoveTShirt()
        {
            _playerTshirt.enabled = false;
        }
    }
}
