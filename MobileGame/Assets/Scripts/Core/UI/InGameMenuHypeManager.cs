using System.Collections;
using System.Collections.Generic;
using Service.Hype;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuHypeManager : MonoBehaviour
{
 private IHypeService _hypeService;
 [SerializeField] private Slider _hypeSlider;
 public void Init(IHypeService hypeService)
 {
  _hypeService = hypeService;
  hypeService.DecreaseHypeEvent += SetSliderValue;
  hypeService.IncreaseHypeEvent += SetSliderValue;
  hypeService.SetHypeEvent += SetSliderValue;
  _hypeSlider.minValue = hypeService.GetMinimumHype();
  _hypeSlider.maxValue = hypeService.GetMaximumHype();
  _hypeSlider.value = hypeService.GetCurrentHype();
 }

 private void SetSliderValue()
 {
  _hypeSlider.value = _hypeService.GetCurrentHype();
 }
 private void SetSliderValue(float amount)
 {
  SetSliderValue();
 }
}
