using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpManger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject speedPowerUp;
    public GameObject healthPowerUp;
    public GameObject damagePowerUp;

    private float _timeTillNextPowerUpSpawn;
    
    void Start()
    {
        _timeTillNextPowerUpSpawn = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        _timeTillNextPowerUpSpawn -= Time.deltaTime;
        if (_timeTillNextPowerUpSpawn <= 0)
        {
            string powerUpType = GameController.instance.GetNextPowerUp();
            Vector3 randomLocation = new Vector3(Random.Range(-4f, 4f), Random.Range(-8f, 8f));
            if (powerUpType == "SpeedInc")
            {
                GameObject instantiatedSpeedObject = Instantiate(speedPowerUp, randomLocation, Quaternion.identity, transform);
                instantiatedSpeedObject.SetActive(true);
            }
            else if (powerUpType == "HealthInc")
            {
                GameObject instantiatedHealthObject = Instantiate(healthPowerUp, randomLocation, Quaternion.identity, transform);
                instantiatedHealthObject.SetActive(true);
            }
            else if (powerUpType == "DamageMulInc")
            {
                GameObject instantiatedDamageObject = Instantiate(damagePowerUp, randomLocation, Quaternion.identity, transform);
                instantiatedDamageObject.SetActive(true);
            }
            _timeTillNextPowerUpSpawn = Random.Range(5f, 10f);
        } 
    }
}
