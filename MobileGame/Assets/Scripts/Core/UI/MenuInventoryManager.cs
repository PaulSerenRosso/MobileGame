using System.Linq;
using Service.Items;
using UnityEngine;

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

        private IItemsService _itemsService;

        public void Setup(IItemsService itemsService, PlayerItemsLinker playerItemsLinker)
        {
            _itemsService = itemsService;
            _itemsService.SetPlayerItemLinker(playerItemsLinker);
            _headScroll.InitializeScroll(
                _itemsService.GetUnlockedItems().Where(i => i.Type == ItemTypeEnum.Head).ToArray(),
                _itemsService.GetPlayerItems().FirstOrDefault(i => i.Key == ItemTypeEnum.Head).Value);
            _shirtScroll.InitializeScroll(
                _itemsService.GetUnlockedItems().Where(i => i.Type == ItemTypeEnum.Shirt).ToArray(),
                _itemsService.GetPlayerItems().FirstOrDefault(i => i.Key == ItemTypeEnum.Shirt).Value);
            _shortScroll.InitializeScroll(
                _itemsService.GetUnlockedItems().Where(i => i.Type == ItemTypeEnum.Short).ToArray(),
                _itemsService.GetPlayerItems().FirstOrDefault(i => i.Key == ItemTypeEnum.Short).Value);
        }

        public void OpenHat()
        {
            _headPanel.SetActive(true);
            _shirtPanel.SetActive(false);
            _shortPanel.SetActive(false);
        }

        public void OpenShirt()
        {
            _headPanel.SetActive(false);
            _shirtPanel.SetActive(true);
            _shortPanel.SetActive(false);
        }

        public void OpenShort()
        {
            _headPanel.SetActive(false);
            _shirtPanel.SetActive(false);
            _shortPanel.SetActive(true);
        }
    }
}