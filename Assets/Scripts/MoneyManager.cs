using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static float TotalMoney { get; private set; }

    public delegate void OnMoneyChangedHandler(float amountAdded, float newTotal);
    public static OnMoneyChangedHandler OnMoneyChanged;

    public static void AddToMoney(float amountToAdd)
    {
        TotalMoney += amountToAdd;
        OnMoneyChanged?.Invoke(amountToAdd, TotalMoney);
    }
}
