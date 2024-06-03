using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy
{
    private GameObject _instance;
    private GameController _gameController;
    private Transform _transform;
    private Rigidbody2D _rb;
    
    private float _health;
    private float _damage;
    private float _speed;

    public BaseEnemy(
        GameObject instance, 
        GameController gameController, 
        float health,
        float damage,
        float speed)
    {
        _instance = instance;
        _gameController = gameController;
        _health = health;
        _damage = damage;
        _speed = speed;

        _transform = _instance.GetComponent<Transform>();
        _rb = _instance.GetComponent<Rigidbody2D>();
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
        
        if (_health <= 0)
        {
            Debug.Log(("Enemy Destroyed"));
            // Delete this instance using destroy
        }
    }

    public void Move()
    {
        // Get the unit vector then mul by speed and game controller speed to determine enemy speed
        this._rb.velocity = (_gameController.GetPlayerPosition() - _transform.position).normalized * (_speed * _gameController.GetEnemySpeed());
    }
}

// public class  
public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    public GameObject basicEnemyObject;

    private Transform _transform;
    private Rigidbody2D _rb;

    private List<BaseEnemy> _basicEnemies;
    
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _transform = GetComponent<Transform>();

        _basicEnemies = new List<BaseEnemy>();
        // Instantiate other gameObjects that are different sprites for different enemies along with different speeds
        _basicEnemies.Add(new BaseEnemy(basicEnemyObject, _gameController, 10f, 1f, 1f));
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (BaseEnemy basicEnemy in _basicEnemies)
        {
            basicEnemy.Move();
        }
        // For checking if a bullet has collided
        // Loop through each of the enemy lists and for each enemy check if collided
    }
}
