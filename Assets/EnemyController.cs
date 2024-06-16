using System;
using UnityEngine;

public class BaseEnemy
{
    private GameController _gameController;
    
    private float _health;
    private float _damage;
    private float _speed;

    public BaseEnemy(
        GameController gameController, 
        float health,
        float damage)
    {
        _gameController = gameController;
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
    
}

public class FastEnemy : BaseEnemy
{
    public FastEnemy(GameController gameController,  float health, float damage) : base(gameController,  health, damage)
    {
        

    }
}

public class BigEnemy : BaseEnemy
{
    public BigEnemy(GameController gameController, float health, float damage) : base(gameController,  health, damage)
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
        this.gameObject.SetActive(true);
        _gameController = gameController.GetComponent<GameController>();
        _rb = GetComponent<Rigidbody2D>();
        Debug.Log("2");
        this._transform = GetComponent<Transform>();
        Debug.Log(GetComponent<Transform>().position);
    }
    
    public void SetEnemyType(string type)
    {
        if (type == "BaseEnemy")
        {
            _speed = 1f;
            _enemy = new BaseEnemy(_gameController, 2f, 1f);
        }
        else if (type == "FastEnemy")
        {
            _speed = 3f;
            _enemy = new FastEnemy(_gameController, 1f, 0.5f);
        }
        else if (type == "BigEnemy")
        {
            _speed = 0.5f;
            _enemy = new BigEnemy(_gameController, 20f, 2f);
        }
            
    }

    public float GetHealth()
    {
        return _enemy.GetHealth();
    }
    
    public void Move()
    {
        // Get the unit vector then mul by speed and game controller speed to determine enemy speed
        this._rb.velocity = (_gameController.GetPlayerPosition() - _transform.position).normalized * (_speed * _gameController.GetEnemySpeed());
    }

    public float GetDistanceFromPlayer()
    {
        Debug.Log("Called 1");
        Debug.Log(this._transform.position);
        return (_gameController.GetPlayerPosition() - this._transform.position).magnitude;
    }

    public float GetPriority()
    {
        return (_speed * 0.1f) * (_enemy.GetHealth() * 0.15f) * (1000 / GetDistanceFromPlayer() * 0.75f);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet collidedBullet = other.gameObject.GetComponent<Bullet>();
            this._enemy.TakeDamage(collidedBullet.GetDamage());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
