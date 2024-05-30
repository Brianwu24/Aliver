using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    
    private Transform _transform;
    private Rigidbody2D _rb;
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

    }

    public void Move(Vector3 directionVector)
    {
        this._rb.velocity = directionVector;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = _gameController.GetPlayerPosition();
        Vector3 directionVector = playerPosition - _transform.position;
        Move(directionVector.normalized * _gameController.GetEnemySpeed());
    }
}
