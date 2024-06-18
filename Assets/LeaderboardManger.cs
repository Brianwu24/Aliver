using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class LeaderboardManger : MonoBehaviour
{
    public static LeaderboardManger instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void SaveLeaderboard()
    {
        if (File.Exists("Assets/Save/Leaderboard.txt"))
        {
            StreamReader sr = new StreamReader("Assets/Save/Leaderboard.txt");
            string previousScoresString = sr.ReadLine();
            string previousHealthString = sr.ReadLine();
            sr.Close();

            string scoreString = previousScoresString + $",{GameController.instance.GetScore()}";
            string healthsString = previousHealthString + $",{Player.instance.GetHealth()}";
            File.Delete("Assets/Save/Leaderboard");
            StreamWriter sw = new StreamWriter("Assets/Save/Leaderboard.txt");
            

            sw.WriteLine(scoreString);
            sw.WriteLine(healthsString);
            sw.Close();
        }
        else
        {
            StreamWriter sw = new StreamWriter("Assets/Save/Leaderboard.txt");

            sw.WriteLine($"{GameController.instance.GetScore()}");
            sw.WriteLine($"{Player.instance.GetHealth()}");
            sw.Close();
        }
            
        
    }

    public (int[], float[]) LoadLeaderboard()
    {
        int[] scores = Array.Empty<int>();
        float[] healths = Array.Empty<float>();
        if (File.Exists("Assets/Save/Leaderboard.txt"))
        {
            StreamReader sr = new StreamReader("Assets/Save/Leaderboard.txt");
            string[] scoresString = sr.ReadLine().Split(",");
            scores = new int[scoresString.Length];
            for (int i=0; i < scoresString.Length; i++)
            {
                scores[i] = int.Parse(scoresString[i], CultureInfo.InvariantCulture);
            }
            
            string[] healthsString = sr.ReadLine().Split(",");
            healths = new float[healthsString.Length];
            for (int i=0; i < healthsString.Length; i++)
            {
                healths[i] = float.Parse(healthsString[i], CultureInfo.InvariantCulture);
            }
        }


        return (scores, healths);
    }

    public string GetLeaderboardText()
    {
        (int[] scores, float[] health) = LoadLeaderboard();

        for (int cursor = 0;  cursor < scores.Length - 1; cursor++)
        {
            for (int i = cursor + 1; i < scores.Length; i++)
            {
                if (scores[cursor] + health[cursor] < scores[i] + health[i])
                {
                    int tmp1 = scores[cursor];
                    float tmp2 = health[cursor];
                    scores[cursor] = scores[i];
                    health[cursor] = health[i];
                    scores[i] = tmp1;
                    health[i] = tmp2;
                }
                
            }
        }

        string leaderboard = "Leaderboard" + Environment.NewLine;

        for (int i = 0; i < scores.Length; i++)
        {
            leaderboard += $"Score: {scores[i]} Health: {health[i]}" + Environment.NewLine;
        }

        return leaderboard;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
