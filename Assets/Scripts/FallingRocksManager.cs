using UnityEngine;

public class FallingRocksManager : MonoBehaviour
{
    [Header("Rock Settings")]
    public GameObject[] rockPrefabs; // Array para los prefabs de las piedras
    public float spawnInterval = 2f; // Intervalo entre las caídas de piedras
    public float spawnHeight = 10f; // Altura desde donde caen las piedras
    public Vector2 spawnRangeX; // Rango X donde pueden caer las piedras

    [Header("Player Settings")]
    public LayerMask playerLayer; // Capa del jugador

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRock();
            timer = 0f; // Reiniciar el temporizador
        }
    }

    void SpawnRock()
    {
        // Elegir un prefab de piedra aleatoriamente
        GameObject selectedRock = rockPrefabs[Random.Range(0, rockPrefabs.Length)];

        // Generar una posición X aleatoria dentro del rango
        float randomX = Random.Range(spawnRangeX.x, spawnRangeX.y);

        // Crear la piedra en la posición aleatoria
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, 0f);
        Instantiate(selectedRock, spawnPosition, Quaternion.identity);
    }
}
