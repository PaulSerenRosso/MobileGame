using System.Linq;
using Service.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service.UI
{
    public class MenuInventoryManager : MonoBehaviour
    {
        [SerializeField] private GameObject _headPanel;
        [SerializeField] private GameObject _shirtPanel;
        [SerializeField] private GameObject _shortPanel;

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
                badgeButton.transform.GetChild(0).GetComponent<Image>().sprite = _badgeSOs[index].SpriteBadge;
                badgeButton.onClick.AddListener(() => OpenPopup(_badgeSOs[index]));
            }
            _itemsService.SetPlayerItemLinker(playerItemsLinker);
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
            _headPanel.SetActive(true);
            _headScroll.UpdateUIInventory();
            _shirtPanel.SetActive(false);
            _shortPanel.SetActive(false);
        }

        public void OpenShirt()
        {
            _headPanel.SetActive(false);
            _shirtPanel.SetActive(true);
            _shirtScroll.UpdateUIInventory();
            _shortPanel.SetActive(false);
        }

        public void OpenShort()
        {
            _headPanel.SetActive(false);
            _shirtPanel.SetActive(false);
            _shortPanel.SetActive(true);
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