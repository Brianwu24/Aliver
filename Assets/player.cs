using System;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;

    public GameObject enemyManager;
    private EnemyManager _enemyManager;

    public GameObject basicBulletObject;
    public GameObject advancedBulletObject;
    public GameObject expertBulletObject;
    private Bullet _bullet;
    
    //movement
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private Transform _transform;
    public float speed = 6f;

    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _rb = GetComponent<Rigidbody2D>();

        string playerBulletType = _gameController.GetBulletType();
        if (playerBulletType == "Basic")
        {
            _bullet = basicBulletObject.GetComponent<Bullet>();
        }
        else if (playerBulletType == "Advanced")
        {
            _bullet = advancedBulletObject.GetComponent<Bullet>();
        }        
        else if (playerBulletType == "Expert")
        {
            _bullet = expertBulletObject.GetComponent<Bullet>();
        }
        
        _enemyManager = enemyManager.GetComponent<EnemyManager>();
        _transform = GetComponent<Transform>();

        _health = 20f;
    }
    
    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDir = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown("space"))
        {
            Shoot();
        }
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_moveDir.x * _gameController.GetPlayerSpeed(), _moveDir.y * _gameController.GetPlayerSpeed());
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        Move();
        
    }

    private void Shoot()
    {
        // Vector3 position = this._transform.position;
        // Vector3 directionVector = (_enemyManager.GetPriorityEnemyPosition() - position).normalized;
        // if (directionVector == new Vector3())
        // {
        //     
        // }
        // Debug.Log(directionVector);
        
        GameObject newBullet = Instantiate(basicBulletObject, _transform.position, Quaternion.identity, _transform);
        newBullet.SetActive(true);
        // newBullet.GetComponent<Bullet>().SetDirectionSpeed(directionVector * _gameController.GetBulletSpeed());
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _health -= other.gameObject.GetComponent<EnemyController>().GetDamage();
            Debug.Log(_health);
            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        InputManagement();
    }
    
}

