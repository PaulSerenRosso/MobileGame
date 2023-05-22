using System.Linq;
using Service.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuInventoryManager : MonoBehaviour, IUpdatable
    {
        [Header("Actual Gear Player")] 
        [SerializeField] private Button _hat;
        [SerializeField] private Button _tshirt;
        [SerializeField] private Button _short;

        [Header("Popup Gear")] 
        [SerializeField] private GameObject _itemPopupPanel;
        [SerializeField] private Image _itemPopupImage;
        [SerializeField] private TextMeshProUGUI _itemPopupTitleText;
        [SerializeField] private TextMeshProUGUI _itemPopupDescriptionText;
        [SerializeField] private Button _equipButton;
        [SerializeField] private TextMeshProUGUI _equipButtonText;

        [Header("Inventory")] 
        [SerializeField] private Button _buttonPrefab;
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private float _speedRotationPlayer = 0.2f;

        private IItemsService _itemsService;

        private GameObject _player;
        private float _screenPosition;

        public void OnUpdate()
        {
            if (Input.touchCount <= 0) return;
            Touch touch = Input.GetTouch(0);
            if (touch.phase is TouchPhase.Ended) return;
            _player.transform.Rotate(Vector3.up * touch.deltaPosition.x * _speedRotationPlayer, Space.World);
        }

        public void Setup(IItemsService itemsService, GameObject player, PlayerItemsLinker playerItemsLinker)
        {
            _itemsService = itemsService;
            _player = player;
            _itemsService.SetPlayerItemLinker(playerItemsLinker);
            UpdateUIInventory();
        }

        private void UpdateUIInventory()
        {
            foreach (var itemSO in _itemsService.GetAllItems())
            {
                var buttonItem = Instantiate(_buttonPrefab, _gridLayout.transform);
                buttonItem.image.sprite = itemSO.SpriteUI;
                buttonItem.onClick.AddListener(() => OpenPopupItem(itemSO));
            }

            foreach (var playerItem in _itemsService.GetPlayerItems())
            {
                switch (playerItem.Value.Type)
                {
                    case ItemTypeEnum.Head:
                        _hat.image.sprite = playerItem.Value.SpriteUI;
                        _hat.onClick.AddListener(() => OpenPopupItem(playerItem.Value));
                        break;
                    case ItemTypeEnum.TShirt:
                        _tshirt.image.sprite = playerItem.Value.SpriteUI;
                        _tshirt.onClick.AddListener(() => OpenPopupItem(playerItem.Value));
                        break;
                    case ItemTypeEnum.Short:
                        _short.image.sprite = playerItem.Value.SpriteUI;
                        _short.onClick.AddListener(() => OpenPopupItem(playerItem.Value));
                        break;
                }
            }
        }

        private void OpenPopupItem(ItemSO itemSO)
        {
            _itemPopupPanel.SetActive(true);
            _itemPopupImage.sprite = itemSO.SpriteUI;
            _itemPopupTitleText.text = itemSO.TitleItem;
            _itemPopupDescriptionText.text = itemSO.DescriptionItem;
            _equipButton.onClick.RemoveAllListeners();
            if (_itemsService.GetPlayerItems().Any(playerItem => playerItem.Value == itemSO))
            {
                _equipButtonText.text = "Unequip";
                _equipButton.onClick.AddListener(() => RemoveItemPlayer(itemSO));
            }
            else
            {
                _equipButtonText.text = "Equip";
                _equipButton.onClick.AddListener(() => UpdateItemPlayer(itemSO));
            }
        }

        public void ClosePopupItem()
        {
            _itemPopupPanel.SetActive(false);
        }

        private void UpdateItemPlayer(ItemSO itemSO)
        {
            _equipButton.onClick.RemoveAllListeners();
            _equipButtonText.text = "Unequip";
            _equipButton.onClick.AddListener(() => RemoveItemPlayer(itemSO));
            _itemsService.SetItemPlayer(itemSO);
            _itemsService.LinkItemPlayer();
            foreach (var playerItem in _itemsService.GetPlayerItems())
            {
                switch (playerItem.Value.Type)
                {
                    case ItemTypeEnum.Head:
                        _hat.image.sprite = playerItem.Value.SpriteUI;
                        break;
                    case ItemTypeEnum.TShirt:
                        _tshirt.image.sprite = playerItem.Value.SpriteUI;
                        break;
                    case ItemTypeEnum.Short:
                        _short.image.sprite = playerItem.Value.SpriteUI;
                        break;
                }
            }
        }

        private void RemoveItemPlayer(ItemSO itemSO)
        {
            _equipButton.onClick.RemoveAllListeners();
            _equipButtonText.text = "Equip";
            _equipButton.onClick.AddListener(() => UpdateItemPlayer(itemSO));
            _itemsService.RemoveItemPlayer(itemSO.Type);
            _itemsService.LinkItemPlayer();
            switch (itemSO.Type)
            {
                case ItemTypeEnum.Head:
                    _hat.image.sprite = null;
                    break;
                case ItemTypeEnum.TShirt:
                    _tshirt.image.sprite = null;
                    break;
                case ItemTypeEnum.Short:
                    _short.image.sprite = null;
                    break;
            }
        }
    }
}