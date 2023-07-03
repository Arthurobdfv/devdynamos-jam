using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] public int maxFill = 3;
    [SerializeField] public int currentFill;

    [SerializeField] private AudioClip audio;

    [SerializeField] public float CurrentFillRate => (float)currentFill / maxFill;

    [SerializeField] private Image fillBar;
    [SerializeField] private bool isRespirando = false;


    // Start is called before the first frame update
    void Start()
    {
        currentFill = 0;
        isRespirando = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var carregavel = collision.GetComponent<ICarregavel>();
        if (collision.gameObject.tag == "Combustivel")
        {
            if (!carregavel.BeingCarried)
            {
                currentFill += 1;
                fillBar.fillAmount = ((float)currentFill / maxFill);
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && isRespirando == true)
        {
            AudioManager.PlaySound(audio);
        }

    }
}
