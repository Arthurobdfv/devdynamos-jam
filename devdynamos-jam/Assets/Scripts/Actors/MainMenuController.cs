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

    void Start()
    {
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


    public void PlayButton()
    {
        SceneManager.LoadScene("MainGame", LoadSceneMode.Additive);
        _naveSprite.SetActive(false);
        SceneManage.Instance.OnStart();
        Destroy(_mainCameraToDisable.gameObject);
        gameObject.SetActive(false);
    }

}
