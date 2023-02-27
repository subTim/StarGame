using Zenject;
using UnityEngine;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerBootstrap _player;
    [SerializeField] private Transform _spawnPoint;

    public override void InstallBindings()
    {
        var playerInstance = Container.InstantiatePrefabForComponent<PlayerBootstrap>(_player, _spawnPoint.position, Quaternion.identity, null);
        Container.Bind<PlayerBootstrap>().FromInstance(playerInstance).AsSingle();
        Container.QueueForInject(_player);
    }
}
