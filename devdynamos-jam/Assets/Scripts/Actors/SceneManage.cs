using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public bool GameStarted => _gameStarted;
    public float InitialAnimationDuration => _initialAnimationDuration;

    [SerializeField] private bool _gameStarted = false;
    [SerializeField] private float _initialAnimationDuration;



    public static SceneManage Instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _gameStarted = false;
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnStart()
    {
        StartCoroutine(GameStartRoutine());
    }

    IEnumerator GameStartRoutine()
    {
        yield return new WaitForSeconds(InitialAnimationDuration);
        _gameStarted = true;
    }
}


internal enum Scenes
{
    MainMenu = 0,
    MainGame = 1
}