using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ChunksSpawner
{
    [SerializeField] private List<Asteroid> prefabs;
    
    [SerializeField] private float chunkSize;
    [SerializeField] private int chunkCapacity;

    [SerializeField] private float maxOffset;
    [SerializeField] private float minOffset;

    [SerializeField] private Transform container;

    private int _chunkHeight, _chunkWidth, _chunkDepth;
    private float _iterationOffset;

    private List<GameObject> _chunkVertexes = new();
    private LinkedList<Asteroid> _activeAsteroids = new ();
    private Pull<Asteroid> _asteroidsPull;

    private Vector3 _defaultOffset;

    public void Init()
    {
        _asteroidsPull = new Pull<Asteroid>(container, true, prefabs, 200);
        _chunkHeight = _chunkDepth =_chunkWidth = chunkCapacity;
        _iterationOffset = chunkSize / chunkCapacity;
        _defaultOffset = new Vector3(GetHalfOffEdge, GetHalfOffEdge, GetHalfOffEdge);
    }

    public void SpawnChunk(Vector3 cords)
    {
        var chunk =  new GameObject(_chunkVertexes.Count + "vertex");
        _chunkVertexes.Add(chunk);


        for (int depth = 0; depth < _chunkDepth; depth++)
        {
            for (int height = 0; height < _chunkHeight; height++)
            {
                for (int width = 0; width < _chunkWidth; width++)
                {
                    var asteroid = _asteroidsPull.GetElement();
                    asteroid.transform.position = GetCurrentIterationOffset(width, height, depth);
                    asteroid.transform.SetParent(chunk.transform);
                    // - GetRandomOffset();
                    width++;
                }
                height++;
            }
            depth++;
        }
        chunk.transform.position = cords - _defaultOffset;
    }
    
    private Vector3 GetRandomOffset() => new(GetRandomValue(), GetRandomValue(), GetRandomValue());
    private float GetRandomValue() => Random.Range(minOffset, maxOffset);
    
    private Vector3 GetCurrentIterationOffset(int x, int y, int z)
    {
        return new Vector3(x * _iterationOffset, y * _iterationOffset,
            z * _iterationOffset);
    }

    private float GetHalfOffEdge => chunkCapacity / 2 * _iterationOffset - _iterationOffset;
    
    private Vector3 GetRandomPosition(Vector3 parentPos)
    {
        parentPos += GetRandomOffset();
        return parentPos;
    }
}