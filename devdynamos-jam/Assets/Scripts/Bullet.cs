using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidade = 10f; // Velocidade do proj�til

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * velocidade; // Define a velocidade inicial do proj�til apenas no eixo X
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Combustivel")
        {
            Destroy(gameObject);
        }

    }
}
