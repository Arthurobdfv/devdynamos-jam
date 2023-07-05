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
    [SerializeField] ParticleSystem oxygenParticle;



    public float diminuicaoDeOxigenioPorSegundo = 0.1f;
    public float o2RecoveryPerSecond = .5f;

    private void Awake()
    {        
        oxygenValue = maxFill;
    }

    // Start is called before the first frame update
    void Start()
    {
        near = true;
        oxygenParticle.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (near == true)
        {
            oxygenValue += o2RecoveryPerSecond * Time.deltaTime;
        }
        else
        {
            oxygenValue -= diminuicaoDeOxigenioPorSegundo * Time.deltaTime;
        }
        oxygenValue =  Mathf.Clamp(oxygenValue, 0, 1);

        oxygenBar.fillAmount = oxygenValue;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Nave"))
        {
            near = true;
            oxygenParticle.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Nave"))
        {
            near = false;
            oxygenParticle.gameObject.SetActive(false);
        }

    }
}
