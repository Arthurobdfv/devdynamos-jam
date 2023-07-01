using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform alvo;
    public float enemyVelocity;

    [Range(0, 100)]
    public float seguirPlayer;

    private void Start()
    {
        Follow();
    }

    void Update()
    {
        //Movimentação do enemy em direção ao player
        Vector2 direction = alvo.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector2 targetPosition = (Vector2)transform.position + direction * enemyVelocity * Time.deltaTime;

        transform.position = targetPosition;
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
