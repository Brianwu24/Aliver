using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;
    
    public GameObject mapController;
    private MapController _mapController;

    // private MapController _mapController;
    //movement
    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _mapController = mapController.GetComponent<MapController>();
        // rb = GetComponent<Rigidbody2D>();
        // _mapController = mapController.GetComponent<MapController>();
    }
    
    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;
    }

    private Vector3 GetMotion()
    {
        return new Vector3(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        // rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        _gameController.Move(this.GetMotion());
        // _mapController.Move(this.GetMotion());
        
    }
    
}

