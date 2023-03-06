using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Chunks 
{
    [SerializeField] private List<Asteroid> prefabs;

    [SerializeField] private float minDistanceToChunk;
    
    [SerializeField] private float chunkSize;
    [SerializeField] private int chunkCapacity;

    [SerializeField] private float maxOffset;
    [SerializeField] private float minOffset;

    [SerializeField] private Transform container;

    private int _chunkHeightCap, _chunkWidthCap, _chunkDepthCap;
    private float _chunkHeight,  _chunkWidth, _chunkDepth;
    private float _iterationOffset;

    private List<GameObject> _vertexes = new();
    private LinkedList<Asteroid> _activeAsteroids = new ();
    private Pull<Asteroid> _asteroidsPull;

    private Vector3 _halfOffset;

    private const int FRAMES_DELAY_BETWEEN_SPAWN = 3;

    public void Init()
    {
        _asteroidsPull = new Pull<Asteroid>(container, true, prefabs, 200);
        
        _iterationOffset = chunkSize / chunkCapacity;
        _chunkHeightCap = _chunkDepthCap =_chunkWidthCap = chunkCapacity;
        _chunkHeight = _chunkWidth = _chunkDepth = GetChunkLength;
        _halfOffset = new Vector3(GetHalfOffEdge, GetHalfOffEdge, GetHalfOffEdge);
    }

    public async void GenerateChunks(Vector3 playerPosition)
    {
        foreach (var pose in CreateStartPoses())
        {
            var posToSpawn = pose - _halfOffset + playerPosition; 
            SpawnChunk(posToSpawn);
            await Task.Delay((int)Time.deltaTime * FRAMES_DELAY_BETWEEN_SPAWN * 1000);
        }
    }

    public void Translate(float removementDistance, Vector3 playerPosition)
    {
        foreach (var vertex in _vertexes)
        {
            var gap = (vertex.transform.position - playerPosition).magnitude;
            Debug.Log(gap);
        }
    }

    public void SpawnChunk(Vector3 cords)
    {
        var chunk =  new GameObject(_vertexes.Count + "vertex");
        _vertexes.Add(chunk);

        for (int depth = 0; depth < _chunkDepthCap; depth++)
        {
            for (int height = 0; height < _chunkHeightCap; height++)
            {
                for (int width = 0; width < _chunkWidthCap; width++)
                {
                    var asteroid = _asteroidsPull.GetElement();
                    asteroid.transform.position = GetCurrentIterationOffset(width, height, depth) + RandomOffset();
                    asteroid.transform.SetParent(chunk.transform);
                    width++;
                }
                height++;
            }
            depth++;
        }
        chunk.transform.position = cords - _halfOffset;
    }
    
    private Vector3 RandomOffset() => new(GetRandomValue(), GetRandomValue(), GetRandomValue());
    private float GetRandomValue() => Random.Range(minOffset, maxOffset);
    
    private Vector3 GetCurrentIterationOffset(int x, int y, int z)
    {
        return new Vector3(x * _iterationOffset, y * _iterationOffset,
            z * _iterationOffset);
    }

    private float GetHalfOffEdge => GetChunkLength / 2 - _iterationOffset;
    private float GetChunkLength => chunkCapacity * _iterationOffset;
    
    private Vector3 GetRandomPosition(Vector3 parentPos)
    {
        parentPos += RandomOffset();
        return parentPos;
    }

    private IEnumerable<Vector3> CreateStartPoses()
    {
        yield return Vector3.zero;
        yield return new Vector3(0,0, -_chunkDepth);
        yield return new Vector3(0,0, _chunkDepth);
        yield return new Vector3(0,_chunkHeight, 0);
        yield return new Vector3(0,-_chunkHeight, 0);
        yield return new Vector3(-_chunkWidth,0, 0);
        yield return new Vector3(_chunkWidth,0, 0);
    }
}