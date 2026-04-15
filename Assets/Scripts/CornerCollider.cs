using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCollider : MonoBehaviour
{
    public void UpdateSize(float sizeAddition)
    {
        transform.localScale += new Vector3(sizeAddition, sizeAddition, sizeAddition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // set to only collide with player in matrix to save on doing layer/tag check
        ScoreManager.AddToScore(5);
        MoneyManager.AddToMoney(0.5f);
    }
}
