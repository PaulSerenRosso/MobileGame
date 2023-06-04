using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutMaskSizer : MonoBehaviour
{
   [SerializeField] private RectTransform _rectTransform;
   [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform currentParent;
   private void Start()
   {
   
         _rectTransform = GetComponent<RectTransform>();
         FitUIElementToScreen();
      

     void FitUIElementToScreen()
     {
        _rectTransform.SetParent(_canvas.transform);
         // Get the reference to the Canvas
         Canvas canvas = GetComponentsInParent<Canvas>()[0];

         // Calculate the screen dimensions
         float screenWidth = canvas.GetComponent<RectTransform>().rect.width;
         float screenHeight = canvas.GetComponent<RectTransform>().rect.height;

         // Set the size of the UI element to fit the screen
         _rectTransform.anchoredPosition = Vector2.zero;
         _rectTransform.sizeDelta = new Vector2(screenWidth, screenHeight);
         _rectTransform.SetParent( currentParent);
      }
   }
}
