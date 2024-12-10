using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        // Verificar si existe una posición de spawn guardada
        if (PlayerPrefs.HasKey("SpawnX") && PlayerPrefs.HasKey("SpawnY"))
        {
            float spawnX = PlayerPrefs.GetFloat("SpawnX");
            float spawnY = PlayerPrefs.GetFloat("SpawnY");

            // Establecer la posición del jugador
            transform.position = new Vector2(spawnX, spawnY);

            // Limpiar los datos de spawn después de usarlos
            PlayerPrefs.DeleteKey("SpawnX");
            PlayerPrefs.DeleteKey("SpawnY");
        }
    }
}
