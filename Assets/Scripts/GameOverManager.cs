using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("Canvas Settings")]
    public GameObject gameOverCanvas; // Canvas de Game Over

    private bool isGameOver = false; // Estado del juego

    void Start()
    {
        // Asegurarse de que el Game Over Canvas esté desactivado al inicio
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return; // Si ya está en estado de Game Over, no hacer nada

        isGameOver = true; // Activar estado de Game Over

        // Activar el Canvas de Game Over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }

        // Pausar el tiempo del juego
        Time.timeScale = 0;
    }

    public void RestartLevel()
    {
        // Reiniciar el tiempo
        Time.timeScale = 1;

        // Cargar la escena actual nuevamente
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }
}
