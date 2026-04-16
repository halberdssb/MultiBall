using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class for bouncing objects in game space
 *
 * Jeff Stevenson
 * 4.14.26
 */

public class Ball : MonoBehaviour
{
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    public float MoneyGainedOnBounce
    {
        get { return _moneyGainedOnBounce; }
        set { _moneyGainedOnBounce = value; }
    }

    private float _speed;
    private int _pointsGainedOnBounce = 1;
    private float _moneyGainedOnBounce = 0.1f;
    
    private LayerMask _collisionMask;
    
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        // random start velocity - 45-degree angle launch in any of four possible directions
        float randomXSign = GetRandomSign();
        float randomYSign = GetRandomSign();
        Vector3 startVelocity = new Vector3(randomXSign, randomYSign, 0).normalized * _speed;
        
        _rb.linearVelocity = startVelocity;
    }
    
    // returns either 1 or -1 randomly
    private float GetRandomSign()
    {
        return Mathf.Sign((Random.value - 0.5f) * 2);
    }
    
    // handles updating points when collisions occur
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check that collision is within collision mask - likely unnecessary, just set collision layers in matrix
        /*if ((_collisionMask.value & (1 << collision.otherCollider.gameObject.layer)) != 0)
        {
            ScoreManager.AddToScore(_pointsGainedOnBounce);
        }*/
        ScoreManager.AddToScore(_pointsGainedOnBounce);
        MoneyManager.AddToMoney(_moneyGainedOnBounce);
    }

    public void UpdateSpeed(float newSpeed)
    {
        _rb.linearVelocity = _rb.linearVelocity.normalized * newSpeed;
    }
    
    public void UpdateMoneyGainedOnBounce(float newAmountGainedOnBounce)
    {
        _moneyGainedOnBounce = newAmountGainedOnBounce;
    }
}
