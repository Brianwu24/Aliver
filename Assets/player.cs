using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;
    
    //movement
    public float moveSpeed;
    Rigidbody2D rb;
    Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
        
    }
    
}

