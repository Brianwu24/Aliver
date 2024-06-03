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
    private float _speed;

    public BaseEnemy(GameObject instance, GameController gameController, float health, float speed)
    {
        _instance = instance;
        _gameController = gameController;
        _health = health;
        _speed = speed;

        _transform = _instance.GetComponent<Transform>();
        _rb = _instance.GetComponent<Rigidbody2D>();
    }

    public void Heal(float additiveHealth)
    {
        this._health += additiveHealth;
    }
    
    public void Damage(float damage)
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
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    public GameObject enemySprite;

    private Transform _transform;
    private Rigidbody2D _rb;

    private BaseEnemy e1;
    
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _transform = GetComponent<Transform>();
        e1 = new BaseEnemy(enemySprite, _gameController, 10f, 1f);
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        e1.Move();
    }
}
