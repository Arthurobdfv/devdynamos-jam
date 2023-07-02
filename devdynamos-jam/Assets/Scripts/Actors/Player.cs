using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerLife;
    [SerializeField] private int playerMaxLife = 3;

    public bool PlayerDead => isDead;


    [SerializeField] private bool isDead = false;

    [SerializeField] private Image lifeBar;


    // Start is called before the first frame update
    void Start()
    {
        playerLife = playerMaxLife;
    }

    private void Update()
    {
        if(playerLife == 0)
        {
            isDead = true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && isDead == false)
        {
            playerLife -= 1;
            lifeBar.fillAmount = ((float)playerLife / playerMaxLife);
        }
    }
}
