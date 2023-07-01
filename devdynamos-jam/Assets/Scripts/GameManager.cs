using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FillBar fill;

    [SerializeField] private GameObject fimDeJogo;

    // Start is called before the first frame update
    void Start()
    {
        fill = FindObjectOfType<FillBar>();
        fimDeJogo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(fill.currentFill == fill.maxFill)
        {
            Winner();
        }
    }

    void Winner()
    {
        fimDeJogo.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        //fim de jogo
    }
}
