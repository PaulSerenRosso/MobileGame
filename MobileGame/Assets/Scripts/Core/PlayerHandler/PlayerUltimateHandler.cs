using System;
using Actions;
using Player.Handler;
using Service.Hype;
using UnityEngine;

public class PlayerUltimateHandler : PlayerHandler
{
    public event Action ActivateUltimateEvent;
    public event Action DeactivateUltimateEvent;
    
    [SerializeField] private AttackPlayerAction _attackPlayerAction;
    [SerializeField] private UltimatePlayerAction _ultimatePlayerAction;

    private bool canUltimate;
    private float _timeBeforeDeactivateInputUltimate;
    private IHypeService _hypeService;

    protected override PlayerAction GetAction()
    {
        return _ultimatePlayerAction;
    }

    public override void InitializeAction()
    {
        
    }

    public override void Setup(params object[] arguments)
    {
        _hypeService = (IHypeService)arguments[0];
        _hypeService.GetPlayerGainUltimateEvent += ActivateInputUltimate;
        _hypeService.GetPlayerLoseUltimateEvent += DeactivateInputUltimate;
        _ultimatePlayerAction.EndActionEvent += DeactivateInputUltimate;
    }

    private void DeactivateInputUltimate()
    {
        canUltimate = false;
        _attackPlayerAction.HitEvent -= _ultimatePlayerAction.MakeAction;
        DeactivateUltimateEvent?.Invoke();
    }

    private void ActivateInputUltimate(float amount)
    {
        canUltimate = true;
        _attackPlayerAction.HitEvent += _ultimatePlayerAction.MakeAction;
        ActivateUltimateEvent?.Invoke();
    }

    private void DeactivateInputUltimate(float amount)
    {
        DeactivateInputUltimate();
    }
}