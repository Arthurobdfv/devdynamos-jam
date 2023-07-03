using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private FillBar fill;
    private Oxygen oxygen;
    private Player lifeplayer;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject loserPanel;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        fill = FindObjectOfType<FillBar>();
        oxygen = FindObjectOfType<Oxygen>();
        lifeplayer = FindObjectOfType<Player>();
        winPanel.SetActive(false);
        loserPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(fill.currentFill == fill.maxFill)
        {
            Winner();
        }

        if (oxygen.isOxygen == false)
        {
            Loser();
        }

        if (lifeplayer.PlayerDead == true)
        {
            Loser();
        }
    }

    void Winner()
    {
        winPanel.SetActive(true);
        //Time.timeScale = 0;
    }

    void Loser()
    {
        loserPanel.SetActive(false);
        //Time.timeScale = 0;
        //fim de jogo Derrota
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
