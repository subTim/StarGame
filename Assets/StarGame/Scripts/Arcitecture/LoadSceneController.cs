using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    [SerializeField] private int _mainSceneIndex = 1;

    public static LoadSceneController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadMainScene();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(_mainSceneIndex);
    }
}
