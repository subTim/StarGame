using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    //[SerializeField] private UiManager _uiService;
    //[SerializeField] private DataManager _dataManager;
    [SerializeField] private LoadSceneController _sceneController;
    //[SerializeField] private UserData _userData;
    //[SerializeField] private RemoteConfigUtil _remoteConfigUtil;
    //[SerializeField] private ObjectPoolingSystem _objectPool;
    //[SerializeField] private GameObject _adsAndAnalitics;

    public override void InstallBindings()
    {
        DontDestroyOnLoad(this);

        //initialisations

        //UI
        //var uiInstance = Container.InstantiatePrefabForComponent<UiManager>(_uiService);
        //Container.Bind<UiManager>().FromInstance(uiInstance).AsSingle();

        //Data
        //var dataManagerInstance = Container.InstantiatePrefabForComponent<DataManager>(_dataManager);
        //Container.Bind<DataManager>().FromInstance(dataManagerInstance).AsSingle();

        ////User data
        //var userDataInstance = Container.InstantiatePrefabForComponent<UserData>(_userData);
        //Container.Bind<UserData>().FromInstance(userDataInstance).AsSingle();

        ////RemoteConfig
        //var remoteConfigUtilInstance = Container.InstantiatePrefabForComponent<RemoteConfigUtil>(_remoteConfigUtil);
        //Container.Bind<RemoteConfigUtil>().FromInstance(remoteConfigUtilInstance).AsSingle();

        //Scene controller
        //var sceneControllerInstance = Container.InstantiatePrefabForComponent<LoadSceneController>(_sceneController);
        //Container.Bind<LoadSceneController>().FromInstance(sceneControllerInstance).AsSingle();

        ////Object pool
        //var objectPoolInstance = Container.InstantiatePrefabForComponent<ObjectPoolingSystem>(_objectPool);
        //Container.Bind<ObjectPoolingSystem>().FromInstance(objectPoolInstance).AsSingle();

        //Ads and analitics
        //GameObject adsAndAnaliticsInstance = Container.InstantiatePrefab(_adsAndAnalitics);
        //Container.Bind<GameObject>().FromInstance(adsAndAnaliticsInstance).AsSingle();

        Utils.UpdateFrameRate();
    }
}
