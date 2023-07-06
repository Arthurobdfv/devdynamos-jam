using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustivel : MonoBehaviour
{
    [SerializeField] private int life = 3;
    [SerializeField] private bool isExplosion = false;

    public AnimationClip explosionAnimationClip;

    [SerializeField] private Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        OnExplode();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            life -= 1;
        }
    }

    void OnExplode()
    {
        if(life == 0)
        {
            isExplosion = true;
            anim.SetBool("isExplosion", isExplosion);
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(explosionAnimationClip.length); // Espera a duração da animação

        Destroy(gameObject); // Destrói o objeto
    }
}
