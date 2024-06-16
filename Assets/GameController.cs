using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float playerSpeed;
    public float playerShootSpeed;
    public float bulletSpeed;
    public GameObject player;
    private Player _player;

    public float enemySpeed;

    public string bulletType;
    
    
    public GameObject mapController;
    private MapController _mapController;
    // Start is called before the first frame update

    void Start()
    {
        // _player = player.GetComponent<Player>();
        _mapController = mapController.GetComponent<MapController>();
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public float GetPlayerShootSpeed()
    {
        return playerShootSpeed;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public string GetBulletType()
    {
        return bulletType;
    }

    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
