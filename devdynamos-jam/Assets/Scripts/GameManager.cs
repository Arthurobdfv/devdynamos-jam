using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Leaderboard leaderboard;
    Scene currentScene = SceneManager.GetActiveScene();

    private int score;

    private FillBar fill;
    private Oxygen oxygen;
    private PlayerMovement lifeplayer;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject loserPanel;


    // Start is called before the first frame update
    void Start()
    {
        leaderboard = FindAnyObjectByType<Leaderboard>();
        Time.timeScale = 1f;

        fill = FindObjectOfType<FillBar>();
        oxygen = FindObjectOfType<Oxygen>();
        lifeplayer = FindObjectOfType<PlayerMovement>();
        winPanel.SetActive(false);
        loserPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(fill.currentFill == fill.maxFill)
        {
            StartCoroutine(Winner());
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

    IEnumerator Winner()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        yield return leaderboard.SubmitScoreRoutine(score);
    }

    void Loser()
    {
        loserPanel.SetActive(true);
        Time.timeScale = 0;
        //fim de jogo Derrota
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ScoreControl()
    {
        score += 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
