using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathF = System.MathF;
using Random = Unity.Mathematics.Random;

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
    public GameObject gameController;
    private GameController _gameController;
    private Player _player;
    private Random _rng;

    public GameObject mapObjects;
    private List<GameObject> _mapObjects;
    private List<GameObject> _trees;

    private float[][] _mapNoise;


    public List<GameObject> CreateTrees(List<GameObject> trees, float[][] noise, float scale)
    {
        List<GameObject> treeList = new List<GameObject>();
        for (int y = 0; y < _mapNoise.GetLength(0); y++)
        {
            for (int x = 0; x < noise[y].Length; x++)
            {
                float probability = noise[y][x];
                if (probability + 0.04>= _rng.NextFloat(0f, 1f))
                {
                    // Choose a random tree
                    treeList.Add(Instantiate(trees[_rng.NextInt(0, trees.Count)], new Vector3(x * scale - 10 + _rng.NextFloat(-1, 1), y * scale - 6 + _rng.NextFloat(-1, 1)), Quaternion.identity,
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
                if (probability + 0.04>= _rng.NextFloat(0f, 1f))
                {
                    // choose a random rock
                    rockList.Add(Instantiate(rocks[_rng.NextInt(0, rocks.Count)], new Vector3(x * scale - 10 + _rng.NextFloat(-1, 1), y * scale - 6 + _rng.NextFloat(-1, 1)), Quaternion.identity,
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
        _rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        _gameController = gameController.GetComponent<GameController>();
        _player = _gameController.GetComponent<Player>();


        _mapObjects = new List<GameObject>();
        foreach (Transform child in mapObjects.transform)
        {
            _mapObjects.Add(child.gameObject);
        }

        float scale = 0.5f;
        _mapNoise = new PerlinNoise(20, 12, scale).GetPerlinNoise();
        CreateTrees(_mapObjects, _mapNoise, scale);

    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
