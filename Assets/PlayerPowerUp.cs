using System;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private string _powerUpType;
    private float _amount;

    public void Start()
    {
        _powerUpType = GameController.instance.GetNextPowerUp();
        if (_powerUpType == "SpeedInc") _amount = 1.1f;
        else if (_powerUpType == "HealthInc") _amount = 5;
        else if (_powerUpType == "DamageMulInc") _amount = 1.15f;
        GameController.instance.GenerateRandomPowerUp();
        
    }

    public string GetPowerUpType()
    {
        return _powerUpType;
    }

    public float GetAmount()
    {
        return _amount;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
