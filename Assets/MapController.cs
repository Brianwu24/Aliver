using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathF = System.MathF;
using Random = Unity.Mathematics.Random;

public class MapChunk
{
    private Random _rng;
    // private List<GameObject> _treeList;
    private Vector3 _startingPos;
    private int _numTrees;

    public MapChunk(List<GameObject> mapObjects, Transform parentTransform, Vector3 startingPos, int numTrees, float size = 10.0f)
    {
        
        this._rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        _startingPos = startingPos;
        _numTrees = numTrees;
        // _startingPos += new Vector3();
        
        
        for (int i=0; i< size; i++)
        {
            // this._treeList.Add(GameObject.Instantiate(tree, _startingPos + new Vector3(), Quaternion.identity, parentTransform));
            GameObject.Instantiate(mapObjects[0], _startingPos + new Vector3(this._rng.NextFloat(-size / 2, size / 2), this._rng.NextFloat(-size / 2, size / 2)), Quaternion.identity, parentTransform);
        }
    }
}
public class MapController : MonoBehaviour
{
    public GameObject gameController;
    private GameController _gameController;
    private Player _player;
    private Random _rng;

    public GameObject mapObjects;

    private List<GameObject> _mapObjects;

    private MapChunk _chunk; 
    // private List<MapChunk> _chunks;
    // Start is called before the first frame update
    
    // private void 
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _player = _gameController.GetComponent<Player>();
        
        _mapObjects = new List<GameObject>();
        foreach (Transform childObject in mapObjects.transform)
        {
            _mapObjects.Add(childObject.gameObject);
        }

        MapChunk _chunk = new MapChunk(_mapObjects, this.transform, new Vector3(), 5);
        // _chunks.Add(new Chunk(tree, this.transform, new Vector3(), 1, 5));
        // this._rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        // _treeList = new List<GameObject>();
        // for (int i=0; i< 20; i++)
        // {
        //     _treeList.Add(Instantiate(tree, new Vector3(this._rng.NextFloat(-10, 10), _rng.NextFloat(-10, 10)), Quaternion.identity, this.transform));
        // }
    }

    public void Move(Vector3 translation)
    {
        this.transform.Translate(translation);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(_rng.NextFloat(0, 2.4f));
        // this.transform.Translate(Vector3.right);
            
    }
}
