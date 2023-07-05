using UnityEngine;

public class SpawnGasoline : MonoBehaviour
{
    public GameObject combustiblePrefab;
    public int numberOfCombustibles = 3;
    public Transform[] spawnPoints;

    private void Start()
    {
        // Exemplo de uso
        SpawnCombustibles();
    }

    private void SpawnCombustibles()
    {
        int numSpawnPoints = spawnPoints.Length;

        // Cria uma cópia dos pontos de spawn
        Transform[] availableSpawnPoints = new Transform[numSpawnPoints];
        System.Array.Copy(spawnPoints, availableSpawnPoints, numSpawnPoints);

        for (int i = 0; i < numberOfCombustibles; i++)
        {
            int randomIndex = Random.Range(0, numSpawnPoints);
            Transform randomSpawnPoint = availableSpawnPoints[randomIndex];
            Vector3 spawnPosition = randomSpawnPoint.position;
            Quaternion spawnRotation = randomSpawnPoint.rotation;

            Instantiate(combustiblePrefab, spawnPosition, spawnRotation);

            // Remove o ponto de spawn usado da lista disponível
            availableSpawnPoints[randomIndex] = availableSpawnPoints[numSpawnPoints - 1];
            numSpawnPoints--;
        }
    }
}
