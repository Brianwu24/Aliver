using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    private Vector3 _playerPosition;

    public GameObject mapController;

    private MapController _mapController;
    // Start is called before the first frame update
    
    void Start()
    {
        _mapController = mapController.GetComponent<MapController>();
        _playerPosition = new Vector3();
    }
    
    public void Move(Vector3 translation)
    {
        _playerPosition += translation;
        _mapController.Move(translation);
    }

    public Vector3 GetPlayerPosition()
    {
        return _playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
