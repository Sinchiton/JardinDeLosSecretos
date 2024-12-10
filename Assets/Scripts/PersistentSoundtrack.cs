using UnityEngine;

public class PersistentSoundtrack : MonoBehaviour
{
    private static PersistentSoundtrack instance;

    void Awake()
    {
        // Verificar si ya existe una instancia del soundtrack
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cargar nuevas escenas
        }
        else
        {
            Destroy(gameObject); // Destruir si ya existe otra instancia
        }
    }
}
