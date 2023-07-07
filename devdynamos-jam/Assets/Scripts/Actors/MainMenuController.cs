using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject _naveSprite;
    [SerializeField] private Camera _mainCameraToDisable;

    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _creditos;
    [SerializeField] private GameObject _config;
    [SerializeField] private GameObject _leaderBoard;
    [SerializeField] private GameObject _nickname;

    private PlayerManager PM;

    void Start()
    {
        PM = FindAnyObjectByType<PlayerManager>();
        _tutorial.SetActive(false);
        _creditos.SetActive(false);
        _mainCameraToDisable = Camera.main;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void OpenTutorial()
    {
        _tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorial.SetActive(false);
    }

    public void OpenCreditos()
    {
        _creditos.SetActive(true);
    }

    public void CloseCreditos()
    {
        _creditos.SetActive(false);
    }

    public void OpenConfig()
    {
        _config.SetActive(true);
    }

    public void CloseConfig()
    {
        _config.SetActive(false);
    }

    public void OpenLeadboard()
    {
        _leaderBoard.SetActive(true);
    }

    public void CloseLeaderBoard()
    {
        _leaderBoard.SetActive(false);
    }

    public void OpenNickname()
    {
        if(PM.playerNameInputfild.text.Length == 0)
        {
            _nickname.SetActive(true);
        }
        else
        {
            PlayButton();
        }

    }

    public void CloseNickna()
    {
        _nickname.SetActive(false);
    }



    public void PlayButton()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Additive);
        _naveSprite.SetActive(false);
        SceneManage.Instance.OnStart();
        Destroy(_mainCameraToDisable.gameObject);
        gameObject.SetActive(false);
    }

}
