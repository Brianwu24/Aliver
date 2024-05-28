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
    private float _scale;

    public PerlinNoise(int width, int height, float scale=0.3f)
    {
        _scale = scale;
        _noise = new float[height][];
        for (int i = 0; i < _noise.Length; i++)
        {
            _noise[i] = new float[width];
        }
        for (float y = 0.0F; y < height; y++)
        {
            for (float x = 0.0F; x < width; x++)
            {
                float sample = Mathf.PerlinNoise(orgX + x * scale, orgY + y * _scale);
                Debug.Log((x, y));
                _noise[(int)y][(int)x] = sample;
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
    private PerlinNoise _mapNoise;

    private List<GameObject> _mapObjects;
    
    
    // private void 
    void Start()
    {
        _gameController = gameController.GetComponent<GameController>();
        _player = _gameController.GetComponent<Player>();

        _mapNoise = new PerlinNoise(10, 6);
        _mapNoise.PrintNoise();

    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
