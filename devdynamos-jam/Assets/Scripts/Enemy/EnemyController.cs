using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform alvo; //O alvo do player será definido na sorte
    public float enemyVelocity; //  Velocidade do player até o inimigo

    public float stopDistance; //Distancia que o inimigo vai parar do alvo

    [SerializeField] private int enemyLife = 2; //Vida do inimigo
    [SerializeField] private bool isAlive = true;

    [Range(0, 100)]
    public float seguirPlayer; //  Chance do inimigo seguir o player

    private void Start()
    {
        Follow();
    }

    void Update()
    {
        //Movimentação do enemy em direção ao player
        Vector2 direction = alvo.position - transform.position;
        float distance = direction.magnitude;

        if(distance > stopDistance && isAlive == true)
        {
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector2 targetPosition = (Vector2)transform.position + direction * enemyVelocity * Time.deltaTime;

            transform.position = targetPosition;
        }

    }

    void Follow() // Quem o inimigo deve seguir?
    {
        if (Random.Range(0, 100) < seguirPlayer)
        {
            // Referenciar a posição do player 
            alvo = GameObject.Find("Player").transform;
        }
        else
        {
            // Referenciar a posição do player 
            alvo = GameObject.Find("Gasolina").transform;
        }
    }
}
