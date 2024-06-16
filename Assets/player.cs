using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;

    public GameObject enemyManager;
    private EnemyManager _enemyManager;

    public GameObject bulletObject;
    private Bullet _bullet;
    
    //movement
    private Rigidbody2D _rb;
    private Vector2 _moveDir;
    private Transform _transform;
    public float speed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _rb = GetComponent<Rigidbody2D>();

        _bullet = bulletObject.GetComponent<Bullet>();
        _enemyManager = enemyManager.GetComponent<EnemyManager>();
        _transform = GetComponent<Transform>();
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
        
        GameObject newBullet = Instantiate(bulletObject, _transform.position, Quaternion.identity, _transform);
        
        // newBullet.GetComponent<Bullet>().SetDirectionSpeed(directionVector * _gameController.GetBulletSpeed());
    }
    
    void Update()
    {
        InputManagement();
    }
    
}

