using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [Tooltip("Set starting page index - starting from 0")]
    public int startingPage;
    [Tooltip("Threshold time for fast swipe in seconds")]
    public float fastSwipeThresholdTime = 0.3f;
    [Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
    public int fastSwipeThresholdMinDistance = 100;
    [Tooltip("How fast will page lerp to target position")]
    public float decelerationRate = 10f;

    // fast swipes should be fast and short. If too long, then it is not fast swipe
    private int _fastSwipeThresholdMaxLimit;

    private ScrollRect _scrollRectComponent;
    private RectTransform _scrollRectRect;
    private RectTransform _container;

    // number of items in container
    private int _itemCount;
    private int _currentItem;

    // whether lerping is in progress and target lerp position
    private bool _lerp;
    private Vector2 _lerpTo;

    // target position of every item
    private List<Vector2> _itemPositions = new();

    // in draggging, when dragging started and where it started
    private bool _dragging;
    private float _timeStamp;
    private Vector2 _startPosition;

    // container with Image components - one Image for each page
    private List<Image> _pageSelectionImages;

    //------------------------------------------------------------------------
    void Start() {
        _scrollRectComponent = GetComponent<ScrollRect>();
        _scrollRectRect = GetComponent<RectTransform>();
        _container = _scrollRectComponent.content;
        _itemCount = _container.childCount;

        _lerp = false;

        // init
        SetPagePositions();
        SetPage(startingPage);
    }

    //------------------------------------------------------------------------
    void Update() {
        // if moving to target position
        Debug.Log("container: " + _container.anchoredPosition);
        if (_lerp) {
            // prevent overshooting with values greater than 1
            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, decelerate);
            // time to stop lerping?
            if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f) {
                // snap to target and stop lerping
                _container.anchoredPosition = _lerpTo;
                _lerp = false;
                // clear also any scrollrect move that may interfere with our lerping
                _scrollRectComponent.velocity = Vector2.zero;
            }
        }
    }

    //------------------------------------------------------------------------
    private void SetPagePositions() {
        int width;
        int offsetX;
        int containerWidth;
        int containerHeight;

        // screen width in pixels of scrollrect window
        width = (int)_scrollRectRect.rect.width;
        // center position of all pages
        offsetX = width / 2;
        // total width
        containerWidth = width * _itemCount;
        containerHeight = (int)_container.sizeDelta.y;
        // limit fast swipe length - beyond this length it is fast swipe no more
        _fastSwipeThresholdMaxLimit = width;

        // set width of container
        Vector2 newSize = new Vector2(containerWidth, containerHeight);
        _container.sizeDelta = newSize;
        Vector2 newPosition = new Vector2(containerWidth / 2, containerHeight / 2);
        _container.anchoredPosition = newPosition;

        // delete any previous settings
        _itemPositions.Clear();

        // iterate through all container childern and set their positions
        for (int i = 0; i < _itemCount; i++) {
            RectTransform child = _container.GetChild(i).GetComponent<RectTransform>();
            Vector2 childPosition;
            childPosition = new Vector2(i * width - containerWidth / 2 + offsetX, 0);
            child.anchoredPosition = childPosition;
            Debug.Log("Child: " + i + ", Position: " + childPosition);
            _itemPositions.Add(-childPosition);
        }
    }

    //------------------------------------------------------------------------
    private void SetPage(int aPageIndex) {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _itemCount - 1);
        Debug.Log("SetPage:" + aPageIndex + " itemPosition: " + _itemPositions[aPageIndex]);
        _container.anchoredPosition = _itemPositions[aPageIndex];
        _currentItem = aPageIndex;
    }

    //------------------------------------------------------------------------
    private void LerpToPage(int aPageIndex) {
        aPageIndex = Mathf.Clamp(aPageIndex, 0, _itemCount - 1);
        _lerpTo = _itemPositions[aPageIndex];
        _lerp = true;
        _currentItem = aPageIndex;
    }

    //------------------------------------------------------------------------
    private void NextScreen() {
        LerpToPage(_currentItem + 1);
    }

    //------------------------------------------------------------------------
    private void PreviousScreen() {
        LerpToPage(_currentItem - 1);
    }

    //------------------------------------------------------------------------
    private int GetNearestPage() {
        // based on distance from current position, find nearest page
        Vector2 currentPosition = _container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestPage = _currentItem;

        for (int i = 0; i < _itemPositions.Count; i++) {
            float testDist = Vector2.SqrMagnitude(currentPosition - _itemPositions[i]);
            if (testDist < distance) {
                distance = testDist;
                nearestPage = i;
            }
        }

        Debug.Log("nearestPage: " + nearestPage);
        return nearestPage;
    }

    //------------------------------------------------------------------------
    public void OnBeginDrag(PointerEventData aEventData) {
        // if currently lerping, then stop it as user is dragging
        _lerp = false;
        // not dragging yet
        _dragging = false;
    }
    
    //------------------------------------------------------------------------
    public void OnDrag(PointerEventData aEventData)
    {
        if (_dragging) return;
        // dragging started
        _dragging = true;
        // save time - unscaled so pausing with Time.scale should not affect it
        _timeStamp = Time.unscaledTime;
        // save current position of container
        _startPosition = _container.anchoredPosition;
    }

    //------------------------------------------------------------------------
    public void OnEndDrag(PointerEventData aEventData) 
    {
        // how much was container's content dragged
        float difference = _startPosition.x - _container.anchoredPosition.x;

        // test for fast swipe - swipe that moves only +/-1 item
        if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime &&
            Mathf.Abs(difference) > fastSwipeThresholdMinDistance &&
            Mathf.Abs(difference) < _fastSwipeThresholdMaxLimit) {
            Debug.Log("Fast Swipe");
            if (difference > 0) {
                NextScreen();
            } else {
                PreviousScreen();
            }
        } else {
            // if not fast time, look to which page we got to
            LerpToPage(GetNearestPage());
        }

        _dragging = false;
    }
}
