using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardText : MonoBehaviour
{
    private TextMeshProUGUI _leaderboardTextMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _leaderboardTextMesh.text = LeaderboardManger.instance.GetLeaderboardText();
    }
}
