using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBar : MonoBehaviour
{
    [SerializeField] public int maxFill = 3;
    [SerializeField] public int currentFill;

    [SerializeField] private Image fillBar;


    // Start is called before the first frame update
    void Start()
    {
        currentFill = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Combustivel")
        {
            currentFill += 1;
            fillBar.fillAmount = ((float)currentFill / maxFill);

        }
    }
}
