using System.Collections;
using System.Collections.Generic;
using Random = Unity.Mathematics.Random;
using UnityEngine;

// public class  
public class EnemyManager : MonoBehaviour
{
    public float enemySpawnTime=4;
    // Start is called before the first frame update
    public GameObject gameController;
    private GameController _gameController;
    public GameObject basicEnemyObject;

    private Transform _transform;
    private Rigidbody2D _rb;

    private List<GameObject> _enemies;
    private float _timeFromLastEnemySpawn;
    private Random _rng;
    
    void Start()
    {
        _rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        _gameController = gameController.GetComponent<GameController>();
        _transform = GetComponent<Transform>();

        _enemies = new List<GameObject>();
        // Instantiate other gameObjects that are different sprites for different enemies along with different speeds
        GameObject newEnemy = Instantiate(basicEnemyObject, new Vector3(), Quaternion.identity, this.transform);
        EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
        newEnemyController.SetEnemyType("FastEnemy");
        _enemies.Add(newEnemy);
    }

    private void _DestroyEnemies()
    {
        if (_enemies is { Count: > 0 })
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (_enemies[i].GetComponent<EnemyController>().GetHealth() <= 0)
                {
                    GameObject enemy = _enemies[i]; 
                    _enemies.RemoveAt(i);
                    Destroy(enemy);
                }
            }
        }
    }

    public Vector3 GetPriorityEnemyPosition()
    {
        // Do some sort of sorting and pick the 0th index to get the best enemy to shoot
        this._DestroyEnemies();
        if (_enemies is { Count: > 0 })
        {
            return _enemies[0].transform.position;
        }
        return new Vector3();
        
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        this._DestroyEnemies();
        if (_timeFromLastEnemySpawn >= enemySpawnTime)
        {

            Vector3 spawnPosition = _gameController.GetPlayerPosition() + new Vector3(_rng.NextFloat(0, 10), _rng.NextFloat(0, 20));
            GameObject newEnemy = Instantiate(basicEnemyObject, spawnPosition, Quaternion.identity, this.transform);
            EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
            float randomNum = _rng.NextFloat(0f, 1f);
            if (0.5 <= randomNum)
            {
                Debug.Log("base spawned");
                newEnemyController.SetEnemyType("BaseEnemy");
            }
            else if (0.5 >= randomNum)
            {
                Debug.Log("fast spawned");
                newEnemyController.SetEnemyType("FastEnemy");
            }
            // else
            // {
            //     
            // }
            _enemies.Add(newEnemy);
            _timeFromLastEnemySpawn = 0;

        }

        _timeFromLastEnemySpawn += Time.deltaTime;
    }
}
