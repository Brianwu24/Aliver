using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;

    public GameObject enemyManager;
    private EnemyManager _enemyManager;

    private string _type;
    private float _damage;
    private Vector3 _directionSpeedVector;
    private Rigidbody2D _rb;

    private bool _isAlive = true;
    void Start()
    {
        _isAlive = true;
        _gameController = gameController.GetComponent<GameController>();
        _enemyManager = enemyManager.GetComponent<EnemyManager>();

        _type = gameObject.name;
        _damage = 1;
        if (_type == "AdvancedBullet")
        {
            _damage = 5;
        }
        else if (_type == "ExpertBullet")
        {
            _damage = 10;
        }

        _rb = GetComponent<Rigidbody2D>();

    }

    public string GetBulletType()
    {
        return _type;
    }

    public Vector3 GetDirectionSpeedVector()
    {
        return _directionSpeedVector;
    }

    public void SetDirectionSpeedVector(Vector3 newDirectionSpeedVector)
    {
        _directionSpeedVector = newDirectionSpeedVector;
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
        if (_directionSpeedVector != null)
        {
            _rb.velocity = _directionSpeedVector;
        }

        if (transform.position.magnitude >= 20)
        {
            _isAlive = false;
        }
    }
}
