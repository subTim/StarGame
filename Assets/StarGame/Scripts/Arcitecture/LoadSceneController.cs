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

    public void LoadRealismScene()
    {
        SceneManager.LoadScene(_mainSceneIndex);
    }
}
