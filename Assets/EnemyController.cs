using System;
using UnityEngine;
// Inheritance
public class BaseEnemy
{
    private float _health;
    private float _damage;
    private float _speed;
    protected Vector3 speedDirectionVector;

    public BaseEnemy(
        float health,
        float damage,
        float speed)
    {
        _health = health;
        _damage = damage;
        _speed = speed;
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

    public float GetSpeed()
    {
        return _speed;
    }

    public void UpdateSpeedDirectionVector(Vector3 newSpeedDirectionVector)
    {
        this.speedDirectionVector = newSpeedDirectionVector;
    }

    public virtual void Update(float distanceFromPlayer){}
    
    public virtual void SetEnraged(){}

    public Vector3 GetSpeedDirectionVector()
    {
        return this.speedDirectionVector;
    }
    
}

public class FastEnemy : BaseEnemy
{
    private float _enragedSpeedMul;
    public FastEnemy( float health, float damage, float speed, float enragedSpeedMul) : base(health, damage, speed)
    {
        _enragedSpeedMul = enragedSpeedMul;

    }

    public override void Update(float distanceFromPlayer)
    {
        // Override the base class' Update method
        if (distanceFromPlayer <= 2)
        {
            this.speedDirectionVector *= _enragedSpeedMul;
        }

    }

    public override void SetEnraged()
    {
        this.speedDirectionVector *= _enragedSpeedMul;
    }
}

public class BigEnemy : BaseEnemy
{
    private float _enragedSpeedMul;
    public BigEnemy(float health, float damage, float speed, float enragedSpeedMul) : base(health, damage, speed)
    {
        _enragedSpeedMul = enragedSpeedMul;
    }
    
    public override void Update( float distanceFromPlayer)
    {
        // Override the base class' Update method
        if (distanceFromPlayer <= 2)
        {
            speedDirectionVector *= _enragedSpeedMul;
        }
    }

    public override void SetEnraged()
    {
        speedDirectionVector *= _enragedSpeedMul;
    }
}
public class EnemyController: MonoBehaviour
{ 
    private Rigidbody2D _rb;
    private Transform _transform;
    
    // Composition
    private BaseEnemy _enemy;

    private string _enemyType;
    private bool _enraged;
    
    
    void Start()
    {
        gameObject.SetActive(true);
        _rb = GetComponent<Rigidbody2D>();

        _enraged = false;
        _transform = GetComponent<Transform>();
    }
    
    public void SetEnemyType(string type)
    {
        _enemyType = type;
        if (type == "BaseEnemy")
        {
            _enemy = new BaseEnemy(2f, 1f, 1.5f);
        }
        else if (type == "FastEnemy")
        {
            _enemy = new FastEnemy(1f, 0.5f,  3f, 3f);
        }
        else if (type == "BigEnemy")
        {
            _enemy = new BigEnemy(20f, 2f, 3f, 1.5f);
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
    
    public void Move(Vector3 speedDirectionVector)
    {
        // Get the unit vector then mul by speed and game controller speed to determine enemy speed
        this._rb.velocity = speedDirectionVector;
    }

    public float GetDistanceFromPlayer()
    {
        return (GameController.instance.GetPlayerPosition() - this._transform.position).magnitude;
    }

    public float GetPriority()
    {
        return (_enemy.GetSpeed() * 2f) * (_enemy.GetHealth() * 0.05f) * (1/GetDistanceFromPlayer() * 100);
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
        return "BaseEnemy";
        
    }

    public void SetEnemyEnraged()
    {
        _enraged = true;
    }

    public bool GetIfEnemyEnraged()
    {
        return _enraged;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _enemy.UpdateSpeedDirectionVector((GameController.instance.GetPlayerPosition() - _transform.position).normalized * GameController.instance.GetEnemySpeed());
        _enemy.Update(GetDistanceFromPlayer());
        if (_enraged)
        {
            _enemy.SetEnraged();
        }
        Move(_enemy.GetSpeedDirectionVector());
    }
}
