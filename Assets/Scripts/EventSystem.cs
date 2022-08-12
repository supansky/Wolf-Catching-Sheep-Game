using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventSystem
{
    public static event Action<GameObject> OnSheepCaught;

    public static event Action OnLevelStart;

    public static event Action<float> OnLeapCooldown;

    public static event Action OnRecord;

    public static void InvokeOnSheepCaught(GameObject gameObject)
    {
        OnSheepCaught?.Invoke(gameObject);
    }
    public static void InvokeOnlevelStart()
    {
        OnLevelStart?.Invoke();
    }
    public static void InvokeOnLeapCooldown(float cd)
    {
        OnLeapCooldown?.Invoke(cd);
    }
    public static void InvokeOnRecord()
    {
        OnRecord?.Invoke();
    }

}
