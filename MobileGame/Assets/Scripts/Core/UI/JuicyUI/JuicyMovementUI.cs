using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JuicyMovementUI : MonoBehaviour
{
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private RectTransform _beginPosMove;
   private Vector2 endPos;
   [SerializeField] private float _moveTime;
   public event Action EndMovementEvent;

   private void Start()
   {
      endPos = _rectTransform.anchoredPosition;
   }

   public void Move()
   {
      _rectTransform.DOKill();
      _rectTransform.anchoredPosition = _beginPosMove.anchoredPosition;
      _rectTransform.DOAnchorPos(endPos, _moveTime).OnComplete(()=>EndMovementEvent?.Invoke());
   }
}
