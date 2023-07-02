using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifeTime = 2f;

    private Rigidbody2D rb;

    public void InitializeBullet(float speed)
    {
        bulletSpeed = speed;
        Destroy(gameObject, 2f); // Destroi a bala depois de 2 segundos caso não colida com nada    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject); // Destroi a bala quando colidir com algum objeto
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletLifeTime); // Destroi a bala depois de um determinado tempo
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * bulletSpeed * Time.deltaTime); // Move a bala na direção para a frente
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed; // Aplica a velocidade para a bala

    }
}
