using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JuicyPopup : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _activateScaleTime;
    [SerializeField] private float _activateReturnToBaseScaleTime;
    [SerializeField] private float _deactivateTime;
    [SerializeField] private float _activateScale;
    [SerializeField] private float _startScale;
    public event Action ActivatePopUpEvent;
    public event Action DeactivatePopUpEvent;
    public void ActivatePopUp()
    {
        gameObject.SetActive(true);
       _rectTransform.localScale *= _startScale;
        _rectTransform.DOKill();
        _rectTransform.DOScale(_activateScale, _activateScaleTime).OnComplete(() =>
        {
            _rectTransform.DOKill();
            _rectTransform.DOScale(1, _activateReturnToBaseScaleTime).OnComplete(() =>
            {
                _rectTransform.DOKill();
                ActivatePopUpEvent?.Invoke();
            });
        });
    }

    public void DeactivatePopUp()
    {
        _rectTransform.DOKill();
        _rectTransform.DOScale(0, _deactivateTime).OnComplete(()=>
        {
            DeactivatePopUpEvent?.Invoke();
            gameObject.SetActive(false);
        });
    }
}
