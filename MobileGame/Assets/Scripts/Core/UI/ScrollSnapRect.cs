using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using Service.Items;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Tooltip("How fast will page lerp to target position")] [SerializeField]
    private float _decelerationRate = 10f;

    [SerializeField] private Image _itemImage;
    
    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRectTransform;
    private RectTransform _container;

    private int _itemCount;
    private int _currentItem;

    private bool _lerp;
    private Vector2 _lerpTo;

    private List<Vector2> _itemPositions = new();
    private ItemSO[] _items;

    private IItemsService _itemsService;

    private List<ItemInventory> _itemsInventory = new();

    public void InitializeScroll(IItemsService itemsService, ItemSO[] items, ItemSO currentItem)
    {
        _itemsService = itemsService;
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRectTransform = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        _items = items;
        int indexCurrentItem = 0;
        if (currentItem is not null) indexCurrentItem = Array.IndexOf(items, currentItem);
        for (var index = 0; index < items.Length; index++)
        {
            var item = items[index];
            var itemImage = Instantiate(_itemImage, _container);
            if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == item) == null)
                itemImage.transform.GetChild(0).gameObject.SetActive(true);
            itemImage.sprite = item.SpriteUI;
            itemImage.transform.SetSiblingIndex(index);
            ItemInventory itemInventory = new ItemInventory(item, itemImage);
            _itemsInventory.Add(itemInventory);
        }

        _itemCount = _container.childCount;

        _lerp = false;

        SetItemPositions();
        SetItem(indexCurrentItem);
    }

    private void Update()
    {
        if (!_lerp) return;
        float decelerate = Mathf.Min(_decelerationRate * Time.deltaTime, 1f);
        _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
        if (!(Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)) return;
        _container.anchoredPosition = _lerpTo;
        _lerp = false;
        _scrollRectComponent.velocity = Vector2.zero;
    }

    public void UpdateUIInventory(ItemSO itemSO = null)
    {
        List<Vector2> itemsUnlockedPos = new List<Vector2>();
        List<Vector2> itemsLockedPos = new List<Vector2>();
        List<ItemInventory> itemsInventoryUnlocked = new List<ItemInventory>();
        List<ItemInventory> itemsInventoryLocked = new List<ItemInventory>();

        int count = 0;
        for (int i = 0; i < _itemsInventory.Count; i++)
        {
            if (!_itemsService.GetUnlockedItems().Contains(_itemsInventory[i].ItemSOInventory)) continue;
            itemsUnlockedPos.Add(_itemPositions[count]);
            _itemsInventory[i].ItemImage.rectTransform.anchoredPosition = -_itemPositions[count];
            itemsInventoryUnlocked.Add(_itemsInventory[i]);
            _itemsInventory[i].ItemImage.transform.GetChild(0).gameObject.SetActive(false);
            count++;
        }

        for (int i = 0; i < _itemsInventory.Count; i++)
        {
            if (_itemsService.GetUnlockedItems().Contains(_itemsInventory[i].ItemSOInventory)) continue;
            itemsLockedPos.Add(_itemPositions[count]);
            _itemsInventory[i].ItemImage.rectTransform.anchoredPosition = -_itemPositions[count];
            itemsInventoryLocked.Add(_itemsInventory[i]);
            _itemsInventory[i].ItemImage.transform.GetChild(0).gameObject.SetActive(true);
            count++;
        }

        _itemPositions.Clear();
        _itemPositions = itemsUnlockedPos.Concat(itemsLockedPos).ToList();
        _itemsInventory.Clear();
        _itemsInventory = itemsInventoryUnlocked.Concat(itemsInventoryLocked).ToList();

        var index = _itemsInventory.FindIndex(i => i.ItemSOInventory == itemSO);
        SetItem(index);
    }

    private void SetItemPositions()
    {
        int width = (int)_scrollRectRectTransform.rect.width;
        int offsetX = width / 3;
        int containerWidth = width * _itemCount / 3;
        int containerHeight = (int)_scrollRectRectTransform.rect.height;

        Vector2 newSize = new Vector2(containerWidth, 0);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        _itemPositions.Clear();

        for (int i = 0; i < _itemCount; i++)
        {
            RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
            var childPosition = new Vector2(-containerWidth / 2 + offsetX * i, 0);
            child.anchoredPosition = childPosition;
            _itemPositions.Add(-child.anchoredPosition);
        }
    }

    private void SetItem(int itemIndex)
    {
        itemIndex = Mathf.Clamp(itemIndex, 0, _itemCount - 1);
        _container.anchoredPosition = _itemPositions[itemIndex];
        _currentItem = itemIndex;
    }

    private void LerpToPage(int itemIndex)
    {
        var currentItemPlayer = _currentItem;
        itemIndex = Mathf.Clamp(itemIndex, 0, _itemCount - 1);
        _lerpTo = _itemPositions[itemIndex];
        _lerp = true;
        _currentItem = itemIndex;
        if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == _itemsInventory[itemIndex].ItemSOInventory) == null) return;
        _itemsService.RemoveItemPlayer(_items[currentItemPlayer].Type);
        _itemsService.SetItemPlayer(_itemsInventory[itemIndex].ItemSOInventory);
        _itemsService.LinkItemPlayer();
    }

    private int GetNearestPage()
    {
        Vector2 currentPosition = _container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestPage = _currentItem;

        for (int i = 0; i < _itemPositions.Count; i++)
        {
            float testDist = Vector2.SqrMagnitude(currentPosition - _itemPositions[i]);
            if (testDist < distance)
            {
                distance = testDist;
                nearestPage = i;
            }
        }

        return nearestPage;
    }

    public void OnBeginDrag(PointerEventData aEventData)
    {
        _lerp = false;
    }

    public void OnEndDrag(PointerEventData aEventData)
    {
        LerpToPage(GetNearestPage());
    }
}

[Serializable]
public class ItemInventory
{
    public ItemSO ItemSOInventory;
    public Image ItemImage;

    public ItemInventory(ItemSO itemSO, Image itemImage)
    {
        ItemSOInventory = itemSO;
        ItemImage = itemImage;
    }
}