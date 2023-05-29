using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JuicyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private  float _minScaleFactor =0.9f;
    [SerializeField] private float _maxScaleFactor = 1.2f;
    [SerializeField] private float _minScaleTime = 0.7f;
    [SerializeField] private float _maxScaleTime = 0.3f;
    [SerializeField] private float _baseScaleTime = 0.3f;
    [SerializeField] private RectTransform _renderer;
    private bool _isNeededCancelFeedback = false;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        _renderer.DOKill();
        _renderer.DOScale(_minScaleFactor, _minScaleTime);
        _isNeededCancelFeedback = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelButtonFeedback();
    }

    private void CancelButtonFeedback()
    {
        if (_isNeededCancelFeedback)
        {
            _renderer.DOKill();
            _renderer.DOScale(_maxScaleFactor, _maxScaleTime) .OnComplete( () => _renderer.DOScale(1, _baseScaleTime));
            _isNeededCancelFeedback = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isNeededCancelFeedback)
        {
            CancelButtonFeedback();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
