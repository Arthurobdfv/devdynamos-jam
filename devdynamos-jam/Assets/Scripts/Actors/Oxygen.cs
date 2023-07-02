using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{

    [SerializeField] private float maxFill = 1;

    [SerializeField] private Image oxygenBar;
    [SerializeField] private bool near = false;

    [SerializeField] public bool isOxygen => oxygenValue > 0;


    [SerializeField] public float oxygenValue;



    public float diminuicaoDeOxigenioPorSegundo = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        oxygenValue = maxFill;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (near == true)
        {
            oxygenValue += diminuicaoDeOxigenioPorSegundo * Time.deltaTime;
        }
        else
        {
            oxygenValue -= diminuicaoDeOxigenioPorSegundo * Time.deltaTime;
        }
        oxygenValue =  Mathf.Clamp(oxygenValue, 0, 1);

        oxygenBar.fillAmount = oxygenValue;


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Nave"))
        {
            near = true;
        }
        else
        {
            near = false;
        }
    }
}
