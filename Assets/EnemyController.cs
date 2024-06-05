using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class BaseEnemy
{
    private GameController _gameController;
    private Transform _transform;
    
    private float _health;
    private float _damage;
    private float _speed;

    public BaseEnemy(
        GameController gameController, 
        Transform transform,
        float health,
        float damage)
    {
        _gameController = gameController;
        _transform = transform;
        _health = health;
        _damage = damage;
    }

    public float GetHealth()
    {
        return this._health;
    }

    public float GetDamage()
    {
        return _damage;
    }

    public void Heal(float additiveHealth)
    {
        this._health += additiveHealth;
    }
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public float GetDistanceFromPlayer()
    {
        return (_gameController.GetPlayerPosition() - _transform.position).magnitude;
    }
    
}

public class FastEnemy : BaseEnemy
{
    public FastEnemy(GameController gameController, Transform transfrom, float health, float damage) : base(gameController, transfrom, health, damage)
    {
        

    }
}
public class EnemyController: MonoBehaviour
{ 
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    
    private Rigidbody2D _rb;
    private Transform _transform;
    private float _speed;
    private BaseEnemy _enemy;
    
    
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
    }
    
    public void SetEnemyType(string type)
    {
        if (type == "BaseEnemy")
        {
            _speed = 1f;
            _enemy = new BaseEnemy(_gameController, _transform, 1f, 1f);
            Debug.Log(_enemy.GetHealth());
        }
        else if (type == "FastEnemy")
        {
            _speed = 1.3f;
            _enemy = new FastEnemy(_gameController,  _transform, 1f, 1f);
        }
            
    }
    
    public void Move()
    {
        // Get the unit vector then mul by speed and game controller speed to determine enemy speed
        this._rb.velocity = (_gameController.GetPlayerPosition() - _transform.position).normalized * (_speed * _gameController.GetEnemySpeed());
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet collidedBullet = other.gameObject.GetComponent<Bullet>();
            this._enemy.TakeDamage(collidedBullet.GetDamage());
            if (0 >= _enemy.GetHealth())
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
