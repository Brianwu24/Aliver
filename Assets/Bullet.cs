using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float _damage;
    void Start()
    {
        _damage = 1;
    }

    public float GetDamage()
    {
        return _damage;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
