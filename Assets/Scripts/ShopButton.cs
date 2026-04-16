using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private float startingCost = 1f;

    [SerializeField] private UnityEvent onPurchase;

    private int _numTimesPurchased;
    private float _currentCost;

    private void Awake()
    {
        // set initial cost
        UpdateCurrentCost();
        UpdatePriceText();
    }
    
    // checks if player has enough money and purchases upgrade if so
    public void TryPurchase()
    {
        if (MoneyManager.TotalMoney >= _currentCost)
        {
            MakePurchase();
        }
    }
    
    // fires purchase event and updates values accordingly
    private void MakePurchase()
    {
        // charge money to manager
        MoneyManager.AddToMoney(-_currentCost);
        
        // purchase upgrade
        onPurchase?.Invoke();
        
        // update cost values
        _numTimesPurchased++;
        UpdateCurrentCost();
        UpdatePriceText();
    }
    
    // increments number of times purchased and updates price accordingly
    private void UpdateCurrentCost()
    {
        _currentCost = (_numTimesPurchased + 1) * startingCost; 
    }
    
    // updates price text
    private void UpdatePriceText()
    {
        priceText.text = "$" + _currentCost.ToString("F2");
    }
}
