using Service.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuInventoryManager : MonoBehaviour
    {
        [SerializeField] private Image _hat;
        [SerializeField] private Image _tshirt;
        [SerializeField] private Image _short;
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private GridLayoutGroup _gridLayout;
        
        private IItemsService _itemsService;
        
        public void Setup(IItemsService itemsService)
        {
            _itemsService = itemsService;
            foreach (var itemSO in _itemsService.GetAllItems())
            {
                var buttonItem = Instantiate(_buttonPrefab, _gridLayout.transform);
                buttonItem.image.sprite = itemSO.SpriteUI;
                buttonItem.onClick.AddListener(() => UpdateItemPlayer(itemSO));
            }

            foreach (var playerItem in _itemsService.GetPlayerItems())
            {
                switch (playerItem.Value.Type)
                {
                    case ItemTypeEnum.Head:
                        _hat.sprite = playerItem.Value.SpriteUI;
                        break;
                    case ItemTypeEnum.TShirt:
                        _tshirt.sprite = playerItem.Value.SpriteUI;
                        break;
                    case ItemTypeEnum.Short:
                        _short.sprite = playerItem.Value.SpriteUI;
                        break;
                }
            }
        }

        public void UpdateItemPlayer(ItemSO itemSO)
        {
            _itemsService.SetItemPlayer(itemSO);
            _itemsService.LinkItemPlayer();
            foreach (var playerItem in _itemsService.GetPlayerItems())
            {
                switch (playerItem.Value.Type)
                {
                    case ItemTypeEnum.Head:
                        _hat.sprite = playerItem.Value.SpriteUI;
                        break;
                    case ItemTypeEnum.TShirt:
                        _tshirt.sprite = playerItem.Value.SpriteUI;
                        break;
                    case ItemTypeEnum.Short:
                        _short.sprite = playerItem.Value.SpriteUI;
                        break;
                }
                
            }
        }
    }
}