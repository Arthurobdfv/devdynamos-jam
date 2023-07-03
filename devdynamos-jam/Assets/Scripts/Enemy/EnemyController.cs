using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform alvo; // O alvo do player será definido na sorte
    public float enemyVelocity; //  Velocidade do player até o inimigo
    [Range(0, 100)]
    public float seguirPlayer; //  Chance do inimigo seguir o player

    public float stopDistance; //Distancia que o inimigo vai parar do alvo

    [SerializeField] private GameObject lifePrefab;

    [Range(0, 100)]
    [SerializeField] private int chancetoDrop;


    #region EnemyLife
    [SerializeField] private int enemyLife = 2; //Vida do inimigo
    [SerializeField] private bool isAlive = true;
    #endregion

    #region Bullet Control
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 1f; // Intervalo de tempo entre os disparos
    private float shootTimer = 0f; // Temporizador para controlar o intervalo de tiro
    #endregion



    private void Start()
    {
        Follow();
    }

    void Update()
    {
        if(enemyLife == 0)
        {
            spawnPowerUp();
            isAlive = false;
            Destroy(gameObject);
        }

        shootTimer += Time.deltaTime;
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

        if (distance <= stopDistance && shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyLife -= 1;
        }
    }

    void Shoot()
    {
        // Criação do projetil
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Follow() // Quem o inimigo deve seguir?
    {
        if (Random.Range(0, 100) < seguirPlayer)
        {
            // Referenciar a posição do player 
            alvo = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            // Referenciar a posição do player 
            alvo = GameObject.FindGameObjectWithTag("Robo").transform;
        }
    }

    void spawnPowerUp()
    {
        if (Random.Range(0, 100) < chancetoDrop)
        {
            Instantiate(lifePrefab, transform.position, Quaternion.identity);
        }

    }
}
