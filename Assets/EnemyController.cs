using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BaseEnemy
{
    private float _health;
    private float _damage;
    private float _speed;

    public BaseEnemy(
        float health,
        float damage)
    {
        _health = health;
        _damage = damage;
    }

    public float GetHealth()
    {
        return this._health;
    }

    public void SetHealth(float newHealth)
    {
        _health = newHealth;
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

    public virtual Vector3 Update(Vector3 speedDirectionVector, float distanceFromPlayer)
    {
        if (distanceFromPlayer <= 5)
        {
            return speedDirectionVector * 1.5f;
        }

        return speedDirectionVector;
    }
    
}

public class FastEnemy : BaseEnemy
{
    private float _enragedSpeedMul;
    public FastEnemy( float health, float damage, float enragedSpeedMul) : base(health, damage)
    {
        _enragedSpeedMul = enragedSpeedMul;

    }

    public override Vector3 Update(Vector3 speedDirectionVector, float distanceFromPlayer)
    {
        // Override the base class' Update method
        if (distanceFromPlayer <= 10)
        {
            return speedDirectionVector * _enragedSpeedMul;
        }

        return speedDirectionVector;
    }
}

public class BigEnemy : BaseEnemy
{
    private float _enragedSpeedMul;
    public BigEnemy(float health, float damage, float enragedSpeedMul) : base(health, damage)
    {
        _enragedSpeedMul = enragedSpeedMul;
    }
    
    public override Vector3 Update(Vector3 speedDirectionVector, float distanceFromPlayer)
    {
        // Override the base class' Update method
        if (distanceFromPlayer <= 2)
        {
            return speedDirectionVector * _enragedSpeedMul;
        }

        return speedDirectionVector;
    }
}
public class EnemyController: MonoBehaviour
{ 
    // Start is called before the first frame update
    private Vector3  _speedDirectionVector;
    
    private Rigidbody2D _rb;
    private Transform _transform;
    private float _speed;
    private BaseEnemy _enemy;

    private string _enemyType;
    
    
    void Start()
    {
        gameObject.SetActive(true);
        _rb = GetComponent<Rigidbody2D>();
        
        
        _transform = GetComponent<Transform>();
    }
    
    public void SetEnemyType(string type)
    {
        _enemyType = type;
        if (type == "BaseEnemy")
        {
            _speed = 1f;
            _enemy = new BaseEnemy(2f, 1f);
        }
        else if (type == "FastEnemy")
        {
            _speed = 3f;
            _enemy = new FastEnemy(1f, 0.5f, 2f);
        }
        else if (type == "BigEnemy")
        {
            _speed = 0.5f;
            _enemy = new BigEnemy(20f, 2f, 1.5f);
        }
            
    }

    public float GetHealth()
    {
        return _enemy.GetHealth();
    }

    public void SetHealth(float newHealth)
    {
        _enemy.SetHealth(newHealth);
    }

    public float GetDamage()
    {
        return _enemy.GetDamage();
    }
    
    public void Move()
    {
        // Get the unit vector then mul by speed and game controller speed to determine enemy speed
        _speedDirectionVector =  (GameController.instance.GetPlayerPosition() - _transform.position).normalized * (_speed * GameController.instance.GetEnemySpeed());

        this._rb.velocity = _speedDirectionVector;
    }

    public float GetDistanceFromPlayer()
    {
        return (GameController.instance.GetPlayerPosition() - this._transform.position).magnitude;
    }

    public float GetPriority()
    {
        return (_speed * 2f) * (_enemy.GetHealth() * 0.05f) * (1/GetDistanceFromPlayer() * 100);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet collidedBullet = other.gameObject.GetComponent<Bullet>();
            this._enemy.TakeDamage(collidedBullet.GetDamage());
        }
    }

    public string GetEnemyType()
    {
        if (_enemyType != null)
        {
            return _enemyType;
        }
        else
        {
            return "BaseEnemy";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        _enemy.Update(_speedDirectionVector, GetDistanceFromPlayer());
    }
}
