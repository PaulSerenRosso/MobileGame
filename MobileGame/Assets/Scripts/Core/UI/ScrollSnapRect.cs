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
    [Tooltip("Threshold time for fast swipe in seconds")] [SerializeField]
    private float _fastSwipeThresholdTime = 0.3f;

    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")] [SerializeField]
    private int _fastSwipeThresholdMinDistance = 100;

    [Tooltip("How fast will page lerp to target position")] [SerializeField]
    private float _decelerationRate = 10f;

    [SerializeField] private Image _itemImage;

    private int _fastSwipeThresholdMaxLimit;

    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRectTransform;
    private RectTransform _container;

    private int _itemCount;
    private int _currentItem;

    private bool _lerp;
    private Vector2 _lerpTo;

    private List<Vector2> _itemPositions = new();
    private ItemSO[] _items;

    private float _timeStamp;
    private Vector2 _startPosition;

    private IItemsService _itemsService;

    public void InitializeScroll(IItemsService itemsService, ItemSO[] items, ItemSO currentItem)
    {
        _itemsService = itemsService;
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRectTransform = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        _items = items;
        int indexCurrentItem = 0;
        if (currentItem is not null) indexCurrentItem = Array.IndexOf(items, currentItem);
        foreach (var item in items)
        {
            var itemImage = Instantiate(_itemImage, _container);
            if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == item) == null) 
                itemImage.transform.GetChild(0).gameObject.SetActive(true);
            itemImage.sprite = item.SpriteUI;
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

    private void SetItemPositions()
    {
        int width = (int)_scrollRectRectTransform.rect.width;
        int offsetX = width / 4;
        int containerWidth = width * (int)Mathf.Ceil(_itemCount / 3);
        int containerHeight = (int)_scrollRectRectTransform.rect.height;
        _fastSwipeThresholdMaxLimit = width;

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
        if (_itemsService.GetUnlockedItems().FirstOrDefault(i => i == _items[itemIndex]) == null) return;
        _itemsService.RemoveItemPlayer(_items[currentItemPlayer].Type);
        _itemsService.SetItemPlayer(_items[itemIndex]);
        _itemsService.LinkItemPlayer();
    }

    private void NextScreen()
    {
        LerpToPage(_currentItem + 1);
    }

    private void PreviousScreen()
    {
        LerpToPage(_currentItem - 1);
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
        _timeStamp = Time.unscaledTime;
        _startPosition = _container.anchoredPosition;
    }

    public void OnEndDrag(PointerEventData aEventData)
    {
        float difference = _startPosition.x - _container.anchoredPosition.x;

        if (Time.unscaledTime - _timeStamp < _fastSwipeThresholdTime &&
            Mathf.Abs(difference) > _fastSwipeThresholdMinDistance &&
            Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit)
        {
            if (difference > 0)
            {
                NextScreen();
            }
            else
            {
                PreviousScreen();
            }
        }
        else
        {
            LerpToPage(GetNearestPage());
        }
    }
}