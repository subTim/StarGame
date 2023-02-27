using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    //[SerializeField] private CameraFolowing _cameraFollower;

    public override void InstallBindings()
    {
        //Container.Bind<CameraFolowing>().FromInstance(_cameraFollower).AsSingle();
        //Container.QueueForInject(_cameraFollower);
    }
}