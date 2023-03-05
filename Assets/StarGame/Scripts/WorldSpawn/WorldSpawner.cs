
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField,Range(1f, 30f)] private float percentOfSpawn;
    [SerializeField] private float removmentdistance;
    [SerializeField] private ChunksSpawner chunksSpawner;

    [SerializeField] private Ship player;
    private Pull<Asteroid> _asteroidsPull;
    
    private CancellationToken _currentCancelation;
    private CancellationTokenSource _sourceToken;

    private const int CANCELLATION_TIME_GAP = 10000;


    private void Start()
    {
        chunksSpawner.Init();
        Spawn();
    }
    
    private void Spawn()
    {
        chunksSpawner.SpawnChunk(player.transform.position);
    }
    
    private async void StartUpDatingWorld()
    {
        CreateNewToken();
        while (true)
        {
            if (_currentCancelation.IsCancellationRequested)
                return;
            
            Task timeToWait = Task.Delay(CANCELLATION_TIME_GAP);
            await timeToWait;
        }
    }

    private void CreateNewToken()
    {
        _sourceToken = new CancellationTokenSource();
        _currentCancelation = _sourceToken.Token;
    }

    private void StopUpdatingWorld()
    {
        _sourceToken.Cancel();
    }
}