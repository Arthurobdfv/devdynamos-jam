using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustivel : MonoBehaviour
{
    [SerializeField] private int life = 3;
    [SerializeField] private bool isExplosion = false;

    public AnimationClip explosionAnimationClip;

    [SerializeField] private AudioClip audio;

    [SerializeField] private Animator anim;

    private ObjetoCarregavel _isCarring;

    private void Start()
    {
        _isCarring = FindAnyObjectByType<ObjetoCarregavel>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        life = Mathf.Clamp(life, 0, 3);
        OnExplode();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet" && _isCarring.isCarrying == false)
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
        AudioManager.PlaySound(audio);
    }
}
