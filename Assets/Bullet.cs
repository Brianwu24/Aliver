using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;

    public GameObject enemyManager;
    private EnemyManager _enemyManager;
    
    private float _damage;
    private Vector3 _directionSpeedVector;
    private Rigidbody2D _rb;
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _enemyManager = enemyManager.GetComponent<EnemyManager>();

        string bulletType = gameObject.name;
        
        _damage = 1;
        _directionSpeedVector = new Vector3();
        _rb = GetComponent<Rigidbody2D>();

        _directionSpeedVector = (_enemyManager.GetPriorityEnemyPosition() -_gameController.GetPlayerPosition()).normalized * _gameController.GetBulletSpeed();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    public float GetDamage()
    {
        return _damage;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = _directionSpeedVector;
    }
}
