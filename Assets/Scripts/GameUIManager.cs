using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour
{
    [Header("Canvas Settings")]
    public GameObject mainMenuCanvas; // Canvas de inicio
    public GameObject gameOverCanvas; // Canvas de Game Over

    [Header("Scene Transition Settings")]
    public Image fadeImage; // Imagen para el efecto de transición
    public float fadeDuration = 1f; // Duración de la transición

    private bool isGameStarted = false; // Estado del juego
    private string currentSceneName;

    private void Start()
    {
        // Configuración inicial
        currentSceneName = SceneManager.GetActiveScene().name;
        mainMenuCanvas.SetActive(true);
        gameOverCanvas.SetActive(false); // Asegúrate de que el canvas de "Game Over" esté oculto
        Time.timeScale = 0; // Pausar el juego al inicio
        StartCoroutine(FadeIn()); // Iniciar con un desvanecimiento
    }

    private void Update()
    {
        // Iniciar el juego al presionar "X"
        if (!isGameStarted && Input.GetKeyDown(KeyCode.X))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        mainMenuCanvas.SetActive(false);
        Time.timeScale = 1; // Reanudar el tiempo
    }

    public void GameOver()
    {
        StartCoroutine(HandleGameOver());
    }

    private IEnumerator HandleGameOver()
    {
        gameOverCanvas.SetActive(true);
        yield return new WaitForSecondsRealtime(3f); // Esperar 3 segundos reales
        gameOverCanvas.SetActive(false); // Ocultar canvas de "Has Muerto"
        StartCoroutine(FadeOutAndRestart());
    }

    private IEnumerator FadeIn()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeOutAndRestart()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }

        // Asegurar que el tiempo se reinicie antes de recargar
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneName);
    }
}
