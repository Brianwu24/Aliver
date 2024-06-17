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

    private bool _isAlive;
    void Start()
    {
        _isAlive = true;
        _gameController = gameController.GetComponent<GameController>();
        _enemyManager = enemyManager.GetComponent<EnemyManager>();

        string bulletType = gameObject.name;
        _damage = 1;
        if (bulletType == "AdvancedBullet")
        {
            _damage = 5;
        }
        else if (bulletType == "ExpertBullet")
        {
            _damage = 10;
        }
        
        _directionSpeedVector = new Vector3();
        _rb = GetComponent<Rigidbody2D>();

        _directionSpeedVector = (_enemyManager.GetPriorityEnemyPosition() -_gameController.GetPlayerPosition()).normalized * _gameController.GetBulletSpeed();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _isAlive = false;
        }
    }

    public float GetDamage()
    {
        return _damage;
    }

    public bool GetAlive()
    {
        return _isAlive;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = _directionSpeedVector;
        if (transform.position.magnitude >= 30)
        {
            _isAlive = false;
        }
    }
}
