using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PerlinNoise
{
    // The origin of the sampled area in the plane.
    public float orgX;
    public float orgY;
    
    private float[][] _noise;

    public PerlinNoise(int width, int height, float scale=0.5f)
    {
        _noise = new float[(int)(height / scale)][];
        for (int i = 0; i < _noise.Length; i++)
        {
            _noise[i] = new float[(int)(width / scale)];
        }

        float sum_samples = 0;
        for (float y = 0.0F; y < _noise.Length; y++)
        {
            for (float x = 0.0F; x < (int)(width / scale); x++)
            {
                float sample = Mathf.PerlinNoise(orgX + x * scale, orgY + y * scale);
                _noise[(int)y][(int)x] = sample;
                sum_samples += sample;
            }
        }

        foreach (var row in _noise)
        {
            for (int x = 0; x < row.Length; x++)
            {
                // Normalize the values to that they all add up to 1
                row[x] /= sum_samples;
            }
        }
    }

    public float[][] GetPerlinNoise()
    {
        return this._noise;
    }

    public void PrintNoise()
    {
        foreach (float[] row in _noise)
        {
            foreach (float value in row)
            {
                Debug.Log(value);
            }
        }
    }
}
public class MapController : MonoBehaviour
{
    public static MapController instance;
    public GameObject gameController;
    private GameController _gameController;
    
    private Player _player;
    
    public GameObject treeObjects;
    private List<GameObject> _treeObjects;
    
    public GameObject rockObjects;
    private List<GameObject> _rockObjects;
    
    private float[][] _mapNoise;


    public List<GameObject> CreateTrees(List<GameObject> trees, float[][] noise, float scale)
    {
        List<GameObject> treeList = new List<GameObject>();
        for (int y = 0; y < _mapNoise.GetLength(0); y++)
        {
            for (int x = 0; x < noise[y].Length; x++)
            {
                float probability = noise[y][x];
                if (probability + 0.04>= Random.Range(0f, 1f))
                {
                    // Choose a random tree
                    treeList.Add(Instantiate(trees[Random.Range(0, trees.Count)], new Vector3(x * scale - 10 + Random.Range(-1f, 1f), y * scale - 6 + Random.Range(-1f, 1f), 1), Quaternion.identity,
                        this.transform));

                    noise[y][x] = 0;
                }
            }
        }

        return treeList;
    }
    public List<GameObject> CreateRocks(List<GameObject> rocks, float[][] noise, float scale)
    {
        List<GameObject> rockList = new List<GameObject>();
        for (int y = 0; y < _mapNoise.GetLength(0); y++)
        {
            int x = 0;
            foreach (float probability in noise[y])
            {
                if (probability + 0.035>= Random.Range(0f, 1f))
                {
                    // choose a random rock
                    rockList.Add(Instantiate(rocks[Random.Range(0, rocks.Count)], new Vector3(x * scale - 10 + Random.Range(-1f, 1f), y * scale - 6 + UnityEngine.Random.Range(-1f, 1f), 1), Quaternion.identity,
                        this.transform));
                }
                x++;
            }
        }
        return rockList;
    }



    // private void 
    void Start()
    {
        // _gameController = gameController.GetComponent<GameController>();
        _player = Player.instance;


        _treeObjects = new List<GameObject>();
        foreach (Transform child in treeObjects.transform)
        {
            _treeObjects.Add(child.gameObject);
        }

        _rockObjects = new List<GameObject>();
        foreach (Transform child in rockObjects.transform)
        {
            _rockObjects.Add(child.gameObject);
        }

        float scale = 0.5f;
        _mapNoise = new PerlinNoise(20, 12, scale).GetPerlinNoise();
        CreateTrees(_treeObjects, _mapNoise, scale);
        CreateRocks(_rockObjects, _mapNoise, scale);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
