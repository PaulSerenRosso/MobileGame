using System;
using DG.Tweening;
using HelperPSR.MonoLoopFunctions;
using HelperPSR.Pool;
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

    [SerializeField] private RectTransform _hypeFillAreaPlayer;
    [SerializeField] private Image _hypeFillPlayer;
    [SerializeField] private Image _hypeFillPlayerLogo;
    [SerializeField] private RectTransform _hypeFillAreaEnemy;
    [SerializeField] private Image _hypeFillEnemy;
    [SerializeField] private Image _hypeFillEnemyLogo;
    [SerializeField] private Image _hypeTauntPlayer;
    [SerializeField] private Image _hypeAttackPlayer;
    [SerializeField] private Image _hypeTauntEnemy;
    [SerializeField] private Image _hypeAttackEnemy;

    [SerializeField] private float _timeHypeTaunt;
    [SerializeField] private float _timeHypeAttack;

    [SerializeField] private ParticleSystem _particleHypePlayer;
    [SerializeField] private ParticleSystem _particleHypeEnemy;
    [SerializeField] private RectTransform _damageHypePlayer;
    [SerializeField] private RectTransform _damageHypeBoss;
    [SerializeField] private float _amountDamageNeedToReceive;
    [SerializeField] private float _durationDamage;

    [SerializeField] private ParticleSystem[] ultimateEnemyFire;
    [SerializeField] private ParticleSystem[] ultimatePlayerFire;

    private Sprite _hypeLogoEnemy;
    private IHypeService _hypeService;
    private IFightService _fightService;

    private float _timerHypeTauntPlayer;
    private float _timerHypeAttackPlayer;
    private float _timerHypeTauntEnemy;
    private float _timerHypeAttackEnemy;

    private Vector2 _oldPositionHypePlayer;
    private Vector2 _newPositionHypePlayer;
    private Vector2 _oldPositionHypeEnemy;
    private Vector2 _newPositionHypeEnemy;

    private Pool<RectTransform> _poolPlayer;
    private Pool<RectTransform> _poolEnemy;

    private float _oldAnchorMaxPlayer = -1;
    private float _amountDamageReceived;

    private event Action _hypeUpdateEvent;

    public void Init(IHypeService hypeService, IFightService fightService)
    {
        _hypeService = hypeService;
        _fightService = fightService;
        fightService.ActivateFightCinematic += () => gameObject.SetActive(false);
        fightService.DeactivateFightCinematic += () => gameObject.SetActive(true);
        fightService.EndFightEvent += i => gameObject.SetActive(false);
        _hypeLogoEnemy = _fightService.GetEnemySO().IconSprite;
        _hypeFillEnemyLogo.sprite = _hypeLogoEnemy;
        hypeService.GetEnemyDecreaseHypeEvent += SetDecreaseEnemySliderValue;
        hypeService.GetEnemyDecreaseHypeEvent += SetEnemySliderDecreaseOutline;
        hypeService.GetEnemyIncreaseHypeEvent += SetIncreaseEnemySliderValue;
        hypeService.GetEnemyIncreaseHypeEvent += SetEnemySliderIncreaseOutline;
        hypeService.GetEnemySetHypeEvent += SetIncreaseEnemySliderValue;
        hypeService.GetPlayerDecreaseHypeEvent += SetDecreasePlayerSliderValue;
        hypeService.GetPlayerDecreaseHypeEvent += SetPlayerSliderDecreaseOutline;
        hypeService.GetPlayerIncreaseHypeEvent += SetIncreasePlayerSliderValue;
        hypeService.GetPlayerIncreaseHypeEvent += SetPlayerSliderIncreaseOutline;
        hypeService.GetPlayerSetHypeEvent += SetIncreasePlayerSliderValue;
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

        _poolPlayer = new Pool<RectTransform>(_damageHypePlayer, 10);
        _poolEnemy = new Pool<RectTransform>(_damageHypeBoss, 10);

        UpdateManager.Register(this);
    }

    private void OnEnable()
    {
        UpdateManager.Register(this);
        ultimatePlayerFire[0].Stop();
        ultimatePlayerFire[1].Stop();
        ultimateEnemyFire[0].Stop();
        ultimateEnemyFire[1].Stop();
    }

    private void OnDisable()
    {
        UpdateManager.UnRegister(this);
    }

    private void UpdateUltimateArea(float amount)
    {
        _hypePlayerSliderOutlineAreaUltimate.value =
            _hypeService.GetMaximumHype() - amount;
        _hypeEnemySliderOutlineAreaUltimate.value = _hypeService.GetMaximumHype() - amount;
    }

    public void OnUpdate()
    {
        _hypeUpdateEvent?.Invoke();
    }

    private void SetIncreaseEnemySliderValue(float amount)
    {
        _hypeEnemySlider.value = _hypeService.GetCurrentHypeEnemy();
        _hypeEnemySliderOutlineAttack.value = _hypeService.GetCurrentHypeEnemy();
        _hypeEnemySliderOutlineTaunt.value = _hypeService.GetCurrentHypeEnemy();
    }

    private void SetDecreaseEnemySliderValue(float amount)
    {
        var oldAnchorMin = _hypeFillEnemy.rectTransform.anchorMin.x;
        _hypeEnemySlider.value = _hypeService.GetCurrentHypeEnemy();
        var decreaseImage = _poolEnemy.GetFromPool();
        var image = decreaseImage.GetComponent<Image>();
        decreaseImage.SetParent(_hypeFillAreaEnemy.transform, false);
        var decreaseImageAnchorMin = decreaseImage.anchorMin;
        decreaseImageAnchorMin.x = oldAnchorMin;
        decreaseImage.anchorMin = decreaseImageAnchorMin;
        var decreaseImageAnchorMax = decreaseImage.anchorMax;
        decreaseImageAnchorMax.x = _hypeFillEnemy.rectTransform.anchorMin.x;
        decreaseImage.anchorMax = decreaseImageAnchorMax;
        image.DOColor(new Color(1, 1, 1, 0), _durationDamage)
            .OnComplete(() => _poolEnemy.AddToPool(decreaseImage));
        _hypeEnemySliderOutlineAttack.value = _hypeService.GetCurrentHypeEnemy();
        _hypeEnemySliderOutlineTaunt.value = _hypeService.GetCurrentHypeEnemy();
    }

    private void SetIncreasePlayerSliderValue(float amount)
    {
        _hypePlayerSlider.value = _hypeService.GetCurrentHypePlayer();
        _hypePlayerSliderOutlineAttack.value = _hypeService.GetCurrentHypePlayer();
        _hypePlayerSliderOutlineTaunt.value = _hypeService.GetCurrentHypePlayer();
    }

    private void SetDecreasePlayerSliderValue(float amount)
    {
        if (_oldAnchorMaxPlayer < 0) _oldAnchorMaxPlayer = _hypeFillPlayer.rectTransform.anchorMax.x;
        _amountDamageReceived += amount;
        _hypePlayerSlider.value = _hypeService.GetCurrentHypePlayer();
        if (_amountDamageReceived > _amountDamageNeedToReceive)
        {
            var decreaseImage = _poolPlayer.GetFromPool();
            var image = decreaseImage.GetComponent<Image>();
            decreaseImage.SetParent(_hypeFillAreaPlayer.transform, false);
            var decreaseImageAnchorMin = decreaseImage.anchorMin;
            decreaseImageAnchorMin.x = _hypeFillPlayer.rectTransform.anchorMax.x;
            decreaseImage.anchorMin = decreaseImageAnchorMin;
            var decreaseImageAnchorMax = decreaseImage.anchorMax;
            decreaseImageAnchorMax.x = _oldAnchorMaxPlayer;
            decreaseImage.anchorMax = decreaseImageAnchorMax;
            image.DOColor(new Color(1, 1, 1, 0), _durationDamage)
                .OnComplete(() => _poolPlayer.AddToPool(decreaseImage));
            _amountDamageReceived = 0;
            _oldAnchorMaxPlayer = -1;
        }
        _hypePlayerSliderOutlineAttack.value = _hypeService.GetCurrentHypePlayer();
        _hypePlayerSliderOutlineTaunt.value = _hypeService.GetCurrentHypePlayer();
    }

    private void SetPlayerSliderGainUltimate(float amount)
    {
        _hypeFillPlayer.sprite = _hypeSpriteUltimate;
        _hypeFillPlayerLogo.sprite = _hypeLogoUltimate;
        ultimatePlayerFire[0].Play();
        ultimatePlayerFire[1].Play();
    }

    private void SetPlayerSliderLoseUltimate(float amount)
    {
        _hypeFillPlayer.sprite = _hypeSpritePlayer;
        _hypeFillPlayerLogo.sprite = _hypeLogoPlayer;
        ultimatePlayerFire[0].Stop();
        ultimatePlayerFire[1].Stop();
    }

    private void SetEnemySliderGainUltimate(float amount)
    {
        _hypeFillEnemy.sprite = _hypeSpriteUltimate;
        _hypeFillEnemyLogo.sprite = _hypeLogoUltimate;
        ultimateEnemyFire[0].Play();
        ultimateEnemyFire[1].Play();
    }

    private void SetEnemySliderLoseUltimate(float amount)
    {
        _hypeFillEnemy.sprite = _hypeSpriteEnemy;
        _hypeFillEnemyLogo.sprite = _hypeLogoEnemy;
        ultimateEnemyFire[0].Stop();
        ultimateEnemyFire[1].Stop();
    }

    private void SetPlayerSliderIncreaseOutline(float obj)
    {
        _hypeUpdateEvent -= TimerTauntPlayer;
        _particleHypePlayer.Play();
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
        _particleHypeEnemy.Play();
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
        _timerHypeTauntPlayer += Time.deltaTime;
        if (_timerHypeTauntPlayer > _timeHypeTaunt)
        {
            _particleHypePlayer.Stop();
        
            _hypeTauntPlayer.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerTauntPlayer;
        }
    }

    private void TimerTauntEnemy()
    {
        _timerHypeTauntEnemy += Time.deltaTime;
        if (_timerHypeTauntEnemy > _timeHypeTaunt)
        {
            _particleHypeEnemy.Stop();
            _hypeTauntEnemy.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerTauntEnemy;
        }
    }

    private void TimerAttackPlayer()
    {
        _timerHypeAttackPlayer += Time.deltaTime;
        if (_timerHypeAttackPlayer > _timeHypeAttack)
        {
            _hypeAttackPlayer.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerAttackPlayer;
        }
    }

    private void TimerAttackEnemy()
    {
        _timerHypeAttackEnemy += Time.deltaTime;
        if (_timerHypeAttackEnemy > _timeHypeAttack)
        {
            _hypeAttackEnemy.color = new Color(1, 1, 1, 0);
            _hypeUpdateEvent -= TimerAttackEnemy;
        }
    }
}