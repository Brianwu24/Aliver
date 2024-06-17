using System;
using System.Collections.Generic;
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

    private List<GameObject> _shotBullets;

    private float _health;
    private bool _isAlive;

    // Start is called before the first frame update
    void Start()
    {
        _isAlive = true;
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

        _shotBullets = new List<GameObject>();
        
        _enemyManager = enemyManager.GetComponent<EnemyManager>();
        _transform = GetComponent<Transform>();

        _health = 20f;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public float GetHealth()
    {
        return _health;
    }

    public void SetHealth(float newHealth)
    {
        if (newHealth > 0)
        {
            _health = newHealth;
        }
        else
        {
            // Just in case something goes wrong with the code
            _isAlive = false;
        }
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
    

    private void FixedUpdate()
    {
        Move();
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(basicBulletObject, _transform.position, Quaternion.identity, _transform);
        newBullet.GetComponent<Bullet>().SetDirectionSpeedVector((_enemyManager.GetPriorityEnemyPosition() -_gameController.GetPlayerPosition()).normalized * _gameController.GetBulletSpeed());
        newBullet.SetActive(true);
        _shotBullets.Add(newBullet);
    }

    public void LoadBullet(Vector3 bulletPosition, Vector3 bulletDirectionSpeedVector, string bulletType)
    {
        GameObject bulletGameObject = basicBulletObject;
        if (bulletType == "AdvancedBullet(Clone)")
        {
            bulletGameObject = advancedBulletObject;
        }
        else if (bulletType == "ExpertBullet(Clone)")
        {
            bulletGameObject = expertBulletObject;
        }

        GameObject loadedBullet = Instantiate(bulletGameObject, bulletPosition, Quaternion.identity, _transform);
        // Debug.Log(bulletDirectionSpeedVector);
        loadedBullet.GetComponent<Bullet>().SetDirectionSpeedVector(bulletDirectionSpeedVector);
        // Debug.Log(("Direction speed" , loadedBullet.GetComponent<Bullet>().GetDirectionSpeedVector()));
        loadedBullet.SetActive(true);
        _shotBullets.Add(loadedBullet);
    }
    
    public List<GameObject> GetBullets()
    {
        return _shotBullets;
    }

    public void PruneBullets()
    {
        // Linear search for destroying bullets
        for (int bulletIndex = 0; bulletIndex < _shotBullets.Count; bulletIndex++)
        {
            GameObject bullet = _shotBullets[bulletIndex];
            if (!bullet.GetComponent<Bullet>().GetAlive())
            {
                _shotBullets.RemoveAt(bulletIndex);
                Destroy(bullet);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _health -= other.gameObject.GetComponent<EnemyController>().GetDamage();
            Debug.Log(_health);
            if (_health <= 0)
            {
                _isAlive = false;
                Debug.Log("Player is dead someone make a death screen or pause screen to handle this");
                // Close the game or put the death screen
                // Todo someone please take care of this!!
            }
        }
    }

    void Update()
    {
        InputManagement();
        PruneBullets();
    }
    
}

