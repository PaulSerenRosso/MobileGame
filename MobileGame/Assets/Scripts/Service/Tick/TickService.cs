using System;
using System.Collections;
using System.Collections.Generic;
using Attributes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Service
{
    public class TickService : ITickeableSwitchableService
{
    private float tickRate = 10f;
    private float tickTimer;
    private static float tickTime;
    public static event Action tickEvent;
    public bool isActive;
// les ref c'est comme un pointeur
    [ServiceInit]
    void SetUp()
    {
        tickTime = 1 / tickRate;
        tickTimer = 0;
        Tick();
        EnabledService();
    }
    public static float getTickTime
    {
        get => tickTime;
    } 
    public async void Tick()
    {
        while (true)
        {
            if (!isActive)
            {
                await UniTask.WaitUntil((PredicateIsActiveService));
            }
            if (tickTimer >= tickTime)
        {
            tickTimer -= tickTime;
            tickEvent?.Invoke();
        }
        else tickTimer += Time.deltaTime;
        await UniTask.DelayFrame(0);
        }
    }

    private bool PredicateIsActiveService()
    {
        return GetIsActiveService;
    }

    public void EnabledService()
    {
        isActive = true;
    }

    public void DisabledService()
    {
        isActive = false;
    }

    public bool GetIsActiveService { get => isActive; }
}
}
