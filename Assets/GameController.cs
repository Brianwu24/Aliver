using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float playerSpeed;
    public float playerShootSpeed;
    public float bulletSpeed;
    
    public GameObject player;
    private Player _player;

    public float enemySpeed;

    public string bulletType;
    
    
    public GameObject mapController;
    private MapController _mapController;

    public GameObject enemyManager;
    private EnemyManager _enemyManager;
    // Start is called before the first frame update

    void Start()
    {
        _player = player.GetComponent<Player>();
        _mapController = mapController.GetComponent<MapController>();
        _enemyManager = enemyManager.GetComponent<EnemyManager>();

        if (File.Exists("Assets/Save/State.txt"))
        {
            // Deal with player data
            //We are on the first line
            StreamReader sr = new StreamReader("Assets/Save/State.txt");

            string[] playerData = sr.ReadLine().Split(",");

            Vector3 loadedPlayerPosition = new Vector3(
                float.Parse(playerData[0], CultureInfo.InvariantCulture.NumberFormat),
                float.Parse(playerData[1], CultureInfo.InvariantCulture.NumberFormat));
            
            // Set the player position
            _player.SetPosition(loadedPlayerPosition);
            
            //Get the player's saved health
            float loadedHealth = float.Parse(playerData[2], CultureInfo.InvariantCulture.NumberFormat);
            _player.SetHealth(loadedHealth);

            //Set bullet type
            bulletType = playerData[3];
            
            //Deal with enemy data
            // We are on the 2nd line
            int numEnemies  = int.Parse(sr.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);
            for (int i = 0; i < numEnemies; i++)
            {
                string[] enemyData = sr.ReadLine().Split(",");
                // x, y, type, health
                Vector3 enemyPosition = new Vector3(
                    float.Parse(enemyData[0], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(enemyData[1], CultureInfo.InvariantCulture.NumberFormat));

                string enemyType = enemyData[2];
                float enemyHealth = float.Parse(enemyData[3], CultureInfo.InvariantCulture.NumberFormat);
                
                _enemyManager.CreateEnemy(enemyPosition, enemyType, enemyHealth);
            }
            
            // Load the bullets
            int numBullets = int.Parse(sr.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);
            for (int bulletIndex = 0; bulletIndex < numBullets; bulletIndex++)
            {
                string[] bulletData = sr.ReadLine().Split(",");
                Vector3 bulletPosition = new Vector3(
                    float.Parse(bulletData[0], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(bulletData[1], CultureInfo.InvariantCulture.NumberFormat)); 
                Vector3 bulletDirectionSpeedVector = new Vector3(
                    float.Parse(bulletData[2], CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(bulletData[3], CultureInfo.InvariantCulture.NumberFormat));
                string bulletType = bulletData[4];
                _player.LoadBullet(bulletPosition, bulletDirectionSpeedVector, bulletType);
                
            }
            sr.Close();
        
        }
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public float GetPlayerShootSpeed()
    {
        return playerShootSpeed;
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public string GetBulletType()
    {
        return bulletType;
    }

    public float GetEnemySpeed()
    {
        return enemySpeed;
    }

    public Vector3 GetPlayerPosition()
    {
        return _player.GetPosition();
    }
    

    public void OnApplicationQuit()
    {
        // Write to a txt file
        // player info (location, health, bulletType)
        //Enemies amount
        //Enemies info (location, enemy type, health)
        
        //Over write the file by deleting, no exception is thrown if the file doesn't exist
        File.Delete("Assets/Save/State.txt");
        
        //Write player data
        StreamWriter sw = new StreamWriter("Assets/Save/State.txt");
        Vector3 playerPosition = _player.GetPosition();
        sw.WriteLine($"{playerPosition.x},{playerPosition.y},{_player.GetHealth()},{bulletType}");

        // Write enemy data
        // enemy amount
        // x, y, type, health
        List<GameObject> enemies = _enemyManager.GetEnemies();
        sw.WriteLine($"{enemies.Count}");
        foreach (GameObject enemy in enemies)
        {
            EnemyController targetEnemyController = enemy.GetComponent<EnemyController>();
            Vector3 enemyLocation = enemy.transform.position;
            string enemyType = targetEnemyController.GetEnemyType();
            float enemyHealth = targetEnemyController.GetHealth();
            sw.WriteLine($"{enemyLocation.x},{enemyLocation.y},{enemyType},{enemyHealth}");
        }
        
        // Make sure that all the bullets are alive
        _player.PruneBullets();
        // write the bullet data
        List<GameObject> shotBullets = _player.GetBullets();
        //Write the amount of bullets in the game that has to be saved
        sw.WriteLine($"{shotBullets.Count}");
        // x, y, direction x, direction y, type
        foreach (GameObject bulletGameObject in shotBullets)
        {
            Bullet bullet = bulletGameObject.GetComponent<Bullet>();
            Vector3 bulletPosition = bullet.transform.position;
            Vector3 bulletDirectionSpeedVector = bullet.GetDirectionSpeedVector();

            sw.WriteLine($"{bulletPosition.x},{bulletPosition.y},{bulletDirectionSpeedVector.x},{bulletDirectionSpeedVector.y},{bullet.GetBulletType()}");
        }
        sw.Close();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
