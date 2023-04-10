using Service.Hype;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuHypeManager : MonoBehaviour
{
    [SerializeField] private Slider _hypePlayerSlider;
    [SerializeField] private Slider _hypeEnemySlider;

    private IHypeService _hypeService;

    public void Init(IHypeService hypeService)
    {
        _hypeService = hypeService;
        hypeService.GetEnemyDecreaseHypeEvent += SetEnemySliderValue;
        hypeService.GetEnemyIncreaseHypeEvent += SetEnemySliderValue;
        hypeService.GetEnemySetHypeEvent += SetEnemySliderValue;
        hypeService.GetPlayerDecreaseHypeEvent += SetPlayerSliderValue;
        hypeService.GetPlayerIncreaseHypeEvent += SetPlayerSliderValue;
        hypeService.GetPlayerSetHypeEvent += SetPlayerSliderValue;
        _hypePlayerSlider.maxValue = hypeService.GetMaximumHype();
        _hypeEnemySlider.maxValue = hypeService.GetMaximumHype();
        _hypeEnemySlider.value = hypeService.GetCurrentHypeEnemy();
        _hypePlayerSlider.value = hypeService.GetCurrentHypePlayer();
    }
    
    private void SetEnemySliderValue(float amount)
    {
        _hypeEnemySlider.value = _hypeService.GetCurrentHypeEnemy();
    }

    private void SetPlayerSliderValue(float amount)
    {
        _hypePlayerSlider.value = _hypeService.GetCurrentHypePlayer();
    }
}