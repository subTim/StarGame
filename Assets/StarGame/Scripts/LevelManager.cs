using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform _asteroidHolder;

    private List<Asteroid> _asteroids;

    private void Awake()
    {
        foreach (Transform transf in _asteroidHolder)
        {
            _asteroids.Add(transf.GetComponent<Asteroid>());
        }
    }
}
