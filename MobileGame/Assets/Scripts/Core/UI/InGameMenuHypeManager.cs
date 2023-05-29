using System;
using HelperPSR.MonoLoopFunctions;
using Service.Fight;
using Service.Hype;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuHypeManager : MonoBehaviour, IUpdatable
{
    [SerializeField] private Slider _hypePlayerSlider;
    [SerializeField] private Slider _hypeEnemySlider;

    [SerializeField] private Sprite _hypeSpriteUltimate;
    [SerializeField] private Sprite _hypeSpritePlayer;
    [SerializeField] private Sprite _hypeSpriteEnemy;

    [SerializeField] private Slider _hypePlayerSliderOutlineAreaUltimate;
    [SerializeField] private Slider _hypePlayerSliderOutlineTaunt;
    [SerializeField] private Slider _hypePlayerSliderOutlineAttack;

    [SerializeField] private Slider _hypeEnemySliderOutlineAreaUltimate;
    [SerializeField] private Slider _hypeEnemySliderOutlineTaunt;
    [SerializeField] private Slider _hypeEnemySliderOutlineAttack;

    [SerializeField] private Sprite _hypeLogoUltimate;
    [SerializeField] private Sprite _hypeLogoPlayer;

    [SerializeField] private Image _hypeFillPlayer;
    [SerializeField] private Image _hypeFillPlayerLogo;
    [SerializeField] private Image _hypeFillEnemy;
    [SerializeField] private Image _hypeFillEnemyLogo;
    [SerializeField] private Image _hypeTauntPlayer;
    [SerializeField] private Image _hypeAttackPlayer;
    [SerializeField] private Image _hypeTauntEnemy;
    [SerializeField] private Image _hypeAttackEnemy;

    [SerializeField] private float _timeHypeTaunt;
    [SerializeField] private float _timeHypeAttack;

    private Sprite _hypeLogoEnemy;
    private IHypeService _hypeService;
    private IFightService _fightService;

    private float _timerHypeTauntPlayer;
    private float _timerHypeAttackPlayer;
    private float _timerHypeTauntEnemy;
    private float _timerHypeAttackEnemy;

    private event Action _hypeUpdateEvent;

    private void OnDisable()
    {
        UpdateManager.UnRegister(this);
    }

    public void Init(IHypeService hypeService, IFightService fightService)
    {
        _hypeService = hypeService;
        _fightService = fightService;
        fightService.ActivateFightCinematic += () => gameObject.SetActive(false);
        fightService.DeactivateFightCinematic += () => gameObject.SetActive(true);
        _hypeLogoEnemy = _fightService.GetEnemySO().Sprite;
        _hypeFillEnemyLogo.sprite = _hypeLogoEnemy;
        hypeService.GetEnemyDecreaseHypeEvent += SetEnemySliderValue;
        hypeService.GetEnemyDecreaseHypeEvent += SetEnemySliderDecreaseOutline;
        hypeService.GetEnemyIncreaseHypeEvent += SetEnemySliderValue;
        hypeService.GetEnemyIncreaseHypeEvent += SetEnemySliderIncreaseOutline;
        hypeService.GetEnemySetHypeEvent += SetEnemySliderValue;
        hypeService.GetPlayerDecreaseHypeEvent += SetPlayerSliderValue;
        hypeService.GetPlayerDecreaseHypeEvent += SetPlayerSliderDecreaseOutline;
        hypeService.GetPlayerIncreaseHypeEvent += SetPlayerSliderValue;
        hypeService.GetPlayerIncreaseHypeEvent += SetPlayerSliderIncreaseOutline;
        hypeService.GetPlayerSetHypeEvent += SetPlayerSliderValue;
        hypeService.GetEnemyGainUltimateEvent += SetEnemySliderGainUltimate;
        hypeService.GetEnemyLoseUltimateEvent += SetEnemySliderLoseUltimate;
        hypeService.GetPlayerGainUltimateEvent += SetPlayerSliderGainUltimate;
        hypeService.GetPlayerLoseUltimateEvent += SetPlayerSliderLoseUltimate;
        _hypePlayerSlider.maxValue = hypeService.GetMaximumHype();
        _hypeEnemySlider.maxValue = hypeService.GetMaximumHype();
        _hypeEnemySliderOutlineAttack.maxValue = hypeService.GetMaximumHype();
        _hypeEnemySliderOutlineTaunt.maxValue = hypeService.GetMaximumHype();
        _hypePlayerSliderOutlineAttack.maxValue = hypeService.GetMaximumHype();
        _hypePlayerSliderOutlineTaunt.maxValue = hypeService.GetMaximumHype();
        _hypeEnemySlider.value = hypeService.GetCurrentHypeEnemy();
        _hypePlayerSlider.value = hypeService.GetCurrentHypePlayer();
        _hypePlayerSliderOutlineAreaUltimate.maxValue = hypeService.GetMaximumHype();
        _hypeEnemySliderOutlineAreaUltimate.maxValue = hypeService.GetMaximumHype();
        UpdateUltimateArea(hypeService.GetUltimateHypeValueEnemy());

        hypeService.UltimateAreaIncreaseEvent += UpdateUltimateArea;
      
        UpdateManager.Register(this);
    }

    private void UpdateUltimateArea(float amount)
    {
        
        _hypePlayerSliderOutlineAreaUltimate.value =
            _hypeService.GetMaximumHype()- amount;
        _hypeEnemySliderOutlineAreaUltimate.value =  _hypeService.GetMaximumHype()- amount;
    }

    public void OnUpdate()
    {
        _hypeUpdateEvent?.Invoke();
    }

    private void SetEnemySliderValue(float amount)
    {
        _hypeEnemySlider.value = _hypeService.GetCurrentHypeEnemy();
        _hypeEnemySliderOutlineAttack.value = _hypeService.GetCurrentHypeEnemy();
        _hypeEnemySliderOutlineTaunt.value = _hypeService.GetCurrentHypeEnemy();
    }

    private void SetPlayerSliderValue(float amount)
    {
        _hypePlayerSlider.value = _hypeService.GetCurrentHypePlayer();
        _hypePlayerSliderOutlineAttack.value = _hypeService.GetCurrentHypePlayer();
        _hypePlayerSliderOutlineTaunt.value = _hypeService.GetCurrentHypePlayer();
    }

    private void SetPlayerSliderGainUltimate(float amount)
    {
        _hypeFillPlayer.sprite = _hypeSpriteUltimate;
        _hypeFillPlayerLogo.sprite = _hypeLogoUltimate;
    }

    private void SetPlayerSliderLoseUltimate(float amount)
    {
        _hypeFillPlayer.sprite = _hypeSpritePlayer;
        _hypeFillPlayerLogo.sprite = _hypeLogoPlayer;
    }

    private void SetEnemySliderGainUltimate(float amount)
    {
        _hypeFillEnemy.sprite = _hypeSpriteUltimate;
        _hypeFillEnemyLogo.sprite = _hypeLogoUltimate;
    }

    private void SetEnemySliderLoseUltimate(float amount)
    {
        _hypeFillEnemy.sprite = _hypeSpriteEnemy;
        _hypeFillEnemyLogo.sprite = _hypeLogoEnemy;
    }

    private void SetPlayerSliderIncreaseOutline(float obj)
    {
        _hypeUpdateEvent -= TimerTauntPlayer;
        _timerHypeTauntPlayer = 0;
        _hypeTauntPlayer.color = new Color(1, 1, 1, 1);
        _hypeUpdateEvent += TimerTauntPlayer;
    }

    private void SetPlayerSliderDecreaseOutline(float obj)
    {
        _hypeUpdateEvent -= TimerAttackPlayer;
        _timerHypeAttackPlayer = 0;
        _hypeAttackPlayer.color = new Color(1, 1, 1, 1);
        _hypeUpdateEvent += TimerAttackPlayer;
    }

    private void SetEnemySliderIncreaseOutline(float amount)
    {
        _hypeUpdateEvent -= TimerTauntEnemy;
        _timerHypeTauntEnemy = 0;
        _hypeTauntEnemy.color = new Color(1, 1, 1, 1);
        _hypeUpdateEvent += TimerTauntEnemy;
    }

    private void SetEnemySliderDecreaseOutline(float amount)
    {
        _hypeUpdateEvent -= TimerAttackEnemy;
        _timerHypeAttackEnemy = 0;
        _hypeAttackEnemy.color = new Color(1, 1, 1, 1);
        _hypeUpdateEvent += TimerAttackEnemy;
    }

    private void TimerTauntPlayer()
    {
        if (_timerHypeTauntPlayer < _timeHypeTaunt) _timerHypeTauntPlayer += Time.deltaTime;
        else
        {
            _hypeTauntPlayer.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerTauntPlayer;
        }
    }

    private void TimerTauntEnemy()
    {
        if (_timerHypeTauntEnemy < _timeHypeTaunt) _timerHypeTauntEnemy += Time.deltaTime;
        else
        {
            _hypeTauntEnemy.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerTauntEnemy;
        }
    }

    private void TimerAttackPlayer()
    {
        if (_timerHypeAttackPlayer < _timeHypeAttack) _timerHypeAttackPlayer += Time.deltaTime;
        else
        {
            _hypeAttackPlayer.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerAttackPlayer;
        }
    }

    private void TimerAttackEnemy()
    {
        if (_timerHypeAttackEnemy < _timeHypeAttack) _timerHypeAttackEnemy += Time.deltaTime;
        else
        {
            _hypeAttackEnemy.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerAttackEnemy;
        }
    }
}