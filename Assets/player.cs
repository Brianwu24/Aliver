using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;
    
    //movement
    private Rigidbody2D _rb;
    private Vector2 _moveDir;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        _moveDir = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        _rb.velocity = new Vector2(_moveDir.x * _gameController.GetPlayerSpeed(), _moveDir.y * _gameController.GetPlayerSpeed());
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

