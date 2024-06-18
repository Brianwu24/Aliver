using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private string _type;
    private float _baseDamage;
    private Vector3 _directionSpeedVector;
    private Rigidbody2D _rb;

    private bool _isAlive = true;
    void Start()
    {
        _isAlive = true;

        _type = gameObject.name;
        _baseDamage = 1;
        if (_type == "AdvancedBullet")
        {
            _baseDamage = 5;
        }
        else if (_type == "ExpertBullet")
        {
            _baseDamage = 10;
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
        return _baseDamage * Player.instance.GetDamageMul();
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
