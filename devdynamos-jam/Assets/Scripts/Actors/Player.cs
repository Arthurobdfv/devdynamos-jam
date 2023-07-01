using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnDistance; // distancia do spawn do inimigo
    [SerializeField] private float spawnFrequency; // Tempo do spawn do inimigo

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine() // Vai ficar spawnando o inimigo na distancia e no tempo definido
    {
        while (true)
        {
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnDistance;
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnFrequency);
        }
    }
}
