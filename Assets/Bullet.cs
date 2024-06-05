using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float _damage;
    private Vector3 _directionSpeedVector;
    private Rigidbody2D _rb;
    void Start()
    {
        _damage = 1;
        _directionSpeedVector = new Vector3();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetDirectionSpeed(Vector3 directionSpeedVector)
    {
        _directionSpeedVector = directionSpeedVector;
    }
    public float GetDamage()
    {
        return _damage;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = _directionSpeedVector;
    }
}
