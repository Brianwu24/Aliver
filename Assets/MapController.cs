using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MathF = System.MathF;
using Random = Unity.Mathematics.Random;

public class MapGenerator : MonoBehaviour
{
    private Random _rng;

    public GameObject tree;
    private List<GameObject> _treeList;
    // Start is called before the first frame update
    void Start()
    {
        this._rng = new Random((uint)UnityEngine.Random.Range(1, 100000));
        _treeList = new List<GameObject>();
        for (int i=0; i< 20; i++)
        {
            _treeList.Add(Instantiate(tree, new Vector3(this._rng.NextFloat(-10, 10), _rng.NextFloat(-10, 10)), Quaternion.identity, this.transform));
        }
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
