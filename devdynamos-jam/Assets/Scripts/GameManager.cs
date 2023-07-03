using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private FillBar fill;
    //private Oxygen oxygen;
    //private Player lifeplayer;

    //[SerializeField] private GameObject fimDeJogo;


    // Start is called before the first frame update
    void Start()
    {

        /*fill = FindObjectOfType<FillBar>();
        oxygen = FindObjectOfType<Oxygen>();
        lifeplayer = FindObjectOfType<Player>();
        fimDeJogo.SetActive(false);

        Time.timeScale = 1f; */
    }

    // Update is called once per frame
    /*void Update()
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
        fimDeJogo.SetActive(true);
        Time.timeScale = 0;
    }

    void Loser()
    {
        Time.timeScale = 0;
        //fim de jogo Derrota
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }*/
}
