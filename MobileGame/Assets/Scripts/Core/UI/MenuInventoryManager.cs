using System.Linq;
using Service.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuInventoryManager : MonoBehaviour
    {
        [SerializeField] private Canvas _headPanel;
        [SerializeField] private Canvas _shirtPanel;
        [SerializeField] private Canvas _shortPanel;

        [SerializeField] private ScrollSnapRect _headScroll;
        [SerializeField] private ScrollSnapRect _shirtScroll;
        [SerializeField] private ScrollSnapRect _shortScroll;

        [SerializeField] private BadgeSO[] _badgeSOs;
        
        [SerializeField] private Button[] _badgeButtons;

        [SerializeField] private JuicyPopup _badgePopup;
        [SerializeField] private Image _imagePopup;
        [SerializeField] private TextMeshProUGUI _titlePopup;
        [SerializeField] private TextMeshProUGUI _descriptionPopup;

        private IItemsService _itemsService;

        public void Setup(IItemsService itemsService, PlayerItemsLinker playerItemsLinker)
        {
            _itemsService = itemsService;
            for (var i = 0; i < _badgeButtons.Length; i++)
            {
                var index = i;
                var badgeButton = _badgeButtons[index];
                badgeButton.transform.GetChild(1).GetComponent<Image>().sprite = _badgeSOs[index].SpriteBadge;
                badgeButton.onClick.AddListener(() => OpenPopup(_badgeSOs[index]));
            }
            _itemsService.SetPlayerItemLinker(playerItemsLinker);
            _itemsService.LinkItemPlayer();
            _headScroll.InitializeScroll(_itemsService,
                _itemsService.GetAllItems().Where(i => i.Type == ItemTypeEnum.Head).ToArray(),
                _itemsService.GetPlayerItems().FirstOrDefault(i => i.Key == ItemTypeEnum.Head).Value);
            _shirtScroll.InitializeScroll(_itemsService,
                _itemsService.GetAllItems().Where(i => i.Type == ItemTypeEnum.Shirt).ToArray(),
                _itemsService.GetPlayerItems().FirstOrDefault(i => i.Key == ItemTypeEnum.Shirt).Value);
            _shortScroll.InitializeScroll(_itemsService,
                _itemsService.GetAllItems().Where(i => i.Type == ItemTypeEnum.Short).ToArray(),
                _itemsService.GetPlayerItems().FirstOrDefault(i => i.Key == ItemTypeEnum.Short).Value);
        }

        public void UpdateScrollRect()
        {
            _headScroll.UpdateUIInventory();
            _shirtScroll.UpdateUIInventory();
            _shortScroll.UpdateUIInventory();
        }

        public void OpenHat()
        {
            _headPanel.sortingOrder = 1;
            _shirtPanel.sortingOrder = 0;
            _shortPanel.sortingOrder = 0;
            _headScroll.UpdateUIInventory();
        }

        public void OpenShirt()
        {
            _headPanel.sortingOrder = 0;
            _shirtPanel.sortingOrder = 1;
            _shortPanel.sortingOrder = 0;
            _shirtScroll.UpdateUIInventory();
        }

        public void OpenShort()
        {
            _headPanel.sortingOrder = 0;
            _shirtPanel.sortingOrder = 0;
            _shortPanel.sortingOrder = 1;
            _shortScroll.UpdateUIInventory();
        }

        private void OpenPopup(BadgeSO badgeSO)
        {
            _badgePopup.ActivatePopUp();
            _imagePopup.sprite = badgeSO.SpriteBadge;
            _titlePopup.text = badgeSO.TitleBadge;
            _descriptionPopup.text = badgeSO.DescriptionBadge;
        }

        public void ClosePopup()
        {
            _badgePopup.gameObject.SetActive(false);
        }
    }
}