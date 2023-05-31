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
 
    [SerializeField] private float _activateScale;
    [SerializeField] private float _startScale;
    [SerializeField] private float _baseScale; 

    public event Action ActivatePopUpEvent;


    private void OnValidate()
    {
        _baseScale = _rectTransform.localScale.x;
    }
    
    public void ActivatePopUp()
    {
        gameObject.SetActive(true);
       _rectTransform.localScale = new Vector3( _startScale, _startScale, _startScale)*_baseScale;

       _rectTransform.DOKill();
        _rectTransform.DOScale(  _baseScale*_activateScale, _activateScaleTime).OnComplete(() =>
        {
            _rectTransform.DOKill();

            _rectTransform.DOScale(_baseScale, _activateReturnToBaseScaleTime).OnComplete(() =>
            {
                _rectTransform.DOKill();
       
                ActivatePopUpEvent?.Invoke();
            });
        });
    }
}
