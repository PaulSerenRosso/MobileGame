using Service.Hype;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuHypeManager : MonoBehaviour
{
    [SerializeField] private Slider _hypeSlider;

    private IHypeService _hypeService;

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