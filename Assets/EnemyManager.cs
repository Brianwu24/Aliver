using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class  
public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    public GameObject basicEnemyObject;

    private Transform _transform;
    private Rigidbody2D _rb;

    private List<GameObject> _enemies;
    
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _transform = GetComponent<Transform>();

        _enemies = new List<GameObject>();
        // Instantiate other gameObjects that are different sprites for different enemies along with different speeds
        GameObject newEnemy = Instantiate(basicEnemyObject, new Vector3(), Quaternion.identity, this.transform);
        EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
        newEnemyController.SetEnemyType("FastEnemy");
        _enemies.Add(newEnemy);
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        // foreach (BaseEnemy basicEnemy in _basicEnemies)
        // {
        //     basicEnemy.Move();
        // }
        // For checking if a bullet has collided
        // Loop through each of the enemy lists and for each enemy check if collided
    }
}
