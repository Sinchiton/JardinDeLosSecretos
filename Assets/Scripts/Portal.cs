using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Tooltip("Nombre exacto de la escena a cargar")]
    public string targetScene; // Escena de destino

    [Tooltip("Posición de spawn en la escena de destino (opcional)")]
    public Vector2 spawnPosition; // Lugar donde aparecerá el jugador en la nueva escena

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto que colisiona es el jugador
        if (collision.CompareTag("Player"))
        {
            // Guardar la posición de spawn para la escena de destino
            PlayerPrefs.SetFloat("SpawnX", spawnPosition.x);
            PlayerPrefs.SetFloat("SpawnY", spawnPosition.y);

            // Cargar la escena objetivo
            SceneManager.LoadScene(targetScene);
        }
    }
}
