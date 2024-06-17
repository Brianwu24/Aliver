using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletPowerUp
{
    protected string _powerUpType;
    public BaseBulletPowerUp(string powerUpType)
    {
        _powerUpType = powerUpType;
    }
    
    public Tuple<string, float> GetPowerUpInfo()
    {
        return new Tuple<string, float>(_powerUpType, 10);
    }
}
public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
