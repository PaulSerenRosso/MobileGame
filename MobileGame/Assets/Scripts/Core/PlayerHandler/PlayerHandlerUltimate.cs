using System;
using System.Collections;
using System.Collections.Generic;
using Actions;
using HelperPSR.RemoteConfigs;
using HelperPSR.Tick;
using Player.Handler;
using Service.Hype;
using UnityEngine;

public class PlayerHandlerUltimate : PlayerHandler
{
    [SerializeField]
    private UltimatePlayerAction _ultimatePlayerAction;

    public event Action ActivateUltimateEvent;
    public event Action DeactivateUltimateEvent;
    private float _timeBeforeDeactivateInputUltimate;
    private IHypeService _hypeService;
    
    [SerializeField]
    private AttackPlayerAction _attackPlayerAction; 
 
   
    private bool canUltimate;
    protected override PlayerAction GetAction()
    {
        return _ultimatePlayerAction;
    }

    public override void InitializeAction()
    {
       
    }
    
    
    public override void Setup(params object[] arguments)
    {
        _hypeService =(IHypeService) arguments[0];
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
        Debug.Log("testfdqsfqd");
        ActivateUltimateEvent?.Invoke();
    }

    private void DeactivateInputUltimate(float amount)
    {
       DeactivateInputUltimate();
    }

    
}

