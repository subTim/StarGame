
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    [SerializeField] private Ship player;
    [SerializeField] private float removmentdistance;
    
    [SerializeField] private Chunks chunks;
    private Pull<Asteroid> _asteroidsPull;
    
    private CancellationToken _currentCancelation;
    private CancellationTokenSource _sourceToken;

    private const int CANCELLATION_TIME_GAP = 10000;


    private void Start()
    {
        chunks.Init();
        Spawn();
        StartUpDatingWorld();
    }
    
    private void Spawn()
    {
        chunks.GenerateChunks(player.transform.position);
    }
    
    private async void StartUpDatingWorld()
    {
        CreateNewToken();
        while (true)
        {
            if (_currentCancelation.IsCancellationRequested)
                return;

            chunks.Translate(removmentdistance, player.transform.position);
            
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