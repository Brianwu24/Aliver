using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSpeedUp: MonoBehaviour
{
    private string _powerUpType;
    private float _speedMul;
    void Start()
    {
        _powerUpType = "PlayerSpeedUp";
        _speedMul = 1.5f;
    }

    public string GetPowerUpType()
    {
        return _powerUpType;
    }

    public float GetSpeedMul()
    {
        return _speedMul;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }
}
