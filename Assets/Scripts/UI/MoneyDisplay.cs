using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    private TextMeshProUGUI _moneyText;
    
    void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();
        MoneyManager.OnMoneyChanged += UpdateMoneyDisplay;
    }

    private void UpdateMoneyDisplay(float amountAdded, float newTotal)
    {
        _moneyText.text = "Money: $" + newTotal.ToString("F2");
    }
}
