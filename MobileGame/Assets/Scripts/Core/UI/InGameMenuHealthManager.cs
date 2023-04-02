using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuHealthManager : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private ILifeable _interfaceLifeable;
    private IDamageable _interfaceDamageable;
    
    public void Init(ILifeable interfaceLifeable, IDamageable interfaceDamageable)
    {
        _interfaceLifeable = interfaceLifeable;
        _interfaceDamageable = interfaceDamageable;
        _interfaceDamageable.ChangeHealth += SetSliderValue;
    }

    private void SetSliderValue()
    {
        _slider.value = _interfaceLifeable.GetHealth() / 100;
    }
}