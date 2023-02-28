using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;

    [SerializeField] private float worldRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadius;
    
    [SerializeField] private float maxOffset;
    [SerializeField] private float minOffset;

    [SerializeField,Range(1, 100)] private float percentOfSpawn;

    private Pull _asteroids;
    private List<GameObject> _activeAsteroids;
    
    private float _xPlusCord;
    private float _xMinusCord;
    
    private float _yPlusCord;
    private float _yMinusCord;

    private float _zMinusCord;
    private float _zPlusCord;

    private Action _addAll;


    private void Awake()
    {
        _asteroids = new Pull(transform, true, prefabs, 200);
    }

    private void Spawn()
    {
        
    }

    private void AddHesitation()
    {
        
    }
    
    private void AddX()
    {
        
    }
    
    private void MinusX()
    {
        
    }
    
    private void AddY()
    {
        
    }
    
    private void MinusY()
    {
        
    }

    private void AddZ()
    {
        
    }
    
    private void MinusZ()
    {
        
    }

    private Vector3 GetRandomPosition(Vector3 parentPos)
    {
        parentPos += GetRandomPosition();
        return parentPos;
    }

    private Vector3 GetRandomPosition() => new(GetRandomValue(), GetRandomValue(), GetRandomValue());
    private float GetRandomValue() => Random.Range(minRadius, maxRadius);
}