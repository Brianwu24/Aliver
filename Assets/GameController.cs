using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public float playerSpeed;
    public float bulletSpeed;
    private int _score;
    private string _nextPowerUp;
    
    public float enemySpeed;

    public string bulletType;
    // Start is called before the first frame update

    void Start()
    {
        this.GenerateRandomPowerUp(); // Create the next random power up in case Player PowerUp decides to use it
        
        bulletType = "BasicBullet";
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
            Player.instance.SetPosition(loadedPlayerPosition);
            
            //Get the player's saved health
            float loadedHealth = float.Parse(playerData[2], CultureInfo.InvariantCulture.NumberFormat);
            Player.instance.SetHealth(loadedHealth);

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
                
                EnemyManager.instance.CreateEnemy(enemyPosition, enemyType, enemyHealth);
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
                Player.instance.LoadBullet(bulletPosition, bulletDirectionSpeedVector, bulletType);
                
            }
            sr.Close();
        }
        instance = this;
    }

    public float GetPlayerSpeed()
    {
        return playerSpeed;
    }

    public void SetPlayerSpeed(float newSpeed)
    {
        playerSpeed = newSpeed;
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
        return Player.instance.GetPosition();
    }

    public void IncScore(int point)
    {
        _score += point;
    }

    public int GetScore()
    {
        return _score;
    }

    public void GenerateRandomPowerUp()
    {
        int randomNum = Random.Range(0, 3);
        if (randomNum == 0) _nextPowerUp = "SpeedInc";
        if (randomNum == 1) _nextPowerUp = "HealthInc";
        if (randomNum == 2) _nextPowerUp = "DamageMulInc"; 
    }

    public string GetNextPowerUp()
    {
        return _nextPowerUp;
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
        Vector3 playerPosition = Player.instance.GetPosition();
        sw.WriteLine($"{playerPosition.x},{playerPosition.y},{Player.instance.GetHealth()},{bulletType}");

        // Write enemy data
        // enemy amount
        // x, y, type, health
        List<GameObject> enemies = EnemyManager.instance.GetEnemies();
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
        Player.instance.PruneBullets();
        // write the bullet data
        List<GameObject> shotBullets = Player.instance.GetBullets();
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
        LeaderboardManger.instance.SaveLeaderboard();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
