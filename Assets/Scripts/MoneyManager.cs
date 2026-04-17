using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static float TotalMoney { get; private set; }

    public delegate void OnMoneyChangedHandler(float amountAdded, float newTotal);
    public static OnMoneyChangedHandler OnMoneyChanged;

    private void Start()
    {
        GameStateManager.OnGameStarted += ResetMoney;
    }
    public static void AddToMoney(float amountToAdd)
    {
        TotalMoney += amountToAdd;
        OnMoneyChanged?.Invoke(amountToAdd, TotalMoney);
    }

    public static void ResetMoney()
    {
        TotalMoney = 0;
        OnMoneyChanged?.Invoke(0, TotalMoney);
    }
}
