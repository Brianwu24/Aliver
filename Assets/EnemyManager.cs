using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

// public class  
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public float enemySpawnTime=0.5f;

    public GameObject wolf1;
    public GameObject wolf2;
    public GameObject wolf3;

    private Transform _transform;
    private Rigidbody2D _rb;

    private List<GameObject> _enemies;
    private float _timeFromLastEnemySpawn;
    
    void Start()
    {
        _transform = GetComponent<Transform>();
        _enemies = new List<GameObject>();
        instance = this;
    }

    public void CreateEnemy(Vector3 spawnPosition, string enemyType, float enemyHealth)
    {
        GameObject enemyObject = wolf1;
        if (enemyType == "FastEnemy")
        {
            enemyObject = wolf2;
        }
        else
        {
            enemyObject = wolf3;
        }
        GameObject newEnemy = Instantiate(enemyObject, spawnPosition, Quaternion.identity, this.transform);
        newEnemy.SetActive(true);
        EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
        newEnemyController.SetEnemyType(enemyType);
        newEnemyController.SetHealth(enemyHealth);
        _enemies.Add(newEnemy);
    }
    

    public List<GameObject> GetEnemies()
    {
        return _enemies;
    }

    private void _DestroyEnemies()
    {
        if (_enemies is { Count: > 0 })
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                EnemyController enemyController = _enemies[i].GetComponent<EnemyController>();
                if (enemyController.GetHealth() <= 0)
                {
                    GameObject enemy = _enemies[i]; 
                    _enemies.RemoveAt(i);
                    Destroy(enemy);

                    string enemyType = enemyController.GetEnemyType();
                    if (enemyType == "BasicEnemy")
                    {
                        GameController.instance.IncScore(1);
                    }
                    else if (enemyType == "FastEnemy")
                    {
                        GameController.instance.IncScore(1);
                    }
                    else if (enemyType == "BigEnemy")
                    {
                        GameController.instance.IncScore(3);
                    }
                }
            }
        }
    }

    public Vector3 GetPriorityEnemyPosition()
    {
        // Do some sort of sorting and pick the 0th index to get the best enemy to shoot
        this._DestroyEnemies();
        if (_enemies != null && _enemies.Count > 0)
        {
            if (_enemies.Count == 1)
            {
                return _enemies[0].transform.position;
            }

            // Sort the _enemies list based on GetPriority
            // Exchange Sort

            for (int cursor = 0; cursor < _enemies.Count - 1; cursor++)
            {
                for (int i = cursor + 1; i < _enemies.Count; i++)
                {

                    GameObject a = _enemies[cursor];
                    GameObject b = _enemies[i];
                    // (_speed * 2f) * (_enemy.GetHealth() * 0.05f) * (1/GetDistanceFromPlayer() * 100);
                    if (a.GetComponent<EnemyController>().GetPriority() <
                        b.GetComponent<EnemyController>().GetPriority())
                    {
                        // What rider has in store for me, I decided not to use this!
                        // store in temp
                        GameObject temp = a; // This will be temp a
                        // Perform swap
                        _enemies[cursor] = b; // Swap a with b
                        _enemies[i] = temp; // Swap b with temp a
                    }

                }
            }
            return _enemies[0].transform.position;
        }

        return new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        
    }

    public void SetRandomEnemyEnraged()
    {
        int randomNum = Random.Range(1, 3);
        string targetEnemy = "BaseEnemy";
        if (randomNum == 1)
        {
            targetEnemy = "FastEnemy";
        }
        else
        {
            targetEnemy = "BigEnemy";
        }
        // Linear search for the first targetEnemy and set it as enraged
        foreach (GameObject enemy in _enemies)
        {
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            // Linear Searching for String, and checking if not enraged
            if (enemyController.GetEnemyType() == targetEnemy && !enemyController.GetIfEnemyEnraged())
            {
                enemyController.SetEnemyEnraged();
            }
        }
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        this._DestroyEnemies();
        if (_timeFromLastEnemySpawn >= enemySpawnTime)
        {

            Vector3 spawnPosition = GameController.instance.GetPlayerPosition() +  new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 15f));
            string enemyType = "BaseEnemy";
            GameObject enemyObject = wolf1;
            float randomNum = Random.Range(0, 1f);
            if (0.3 >= randomNum) // If we want a different enemy other than the base one
            {
                enemyObject = wolf2;
                enemyType = "FastEnemy";
            }
            else if (0.3 <= randomNum && randomNum <= 0.5)
            {
                enemyObject = wolf3;
                enemyType = "BigEnemy";
            }
            
            GameObject newEnemy = Instantiate(enemyObject, spawnPosition, Quaternion.identity, this.transform);
            newEnemy.SetActive(true);
            EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
            newEnemyController.SetEnemyType(enemyType);
            _enemies.Add(newEnemy);
            _timeFromLastEnemySpawn = 0;

        }

        _timeFromLastEnemySpawn += Time.deltaTime;
    }
}
