using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform cameraTransform; // Cámara principal
    public float parallaxFactor; // Velocidad del efecto (menor para el fondo más lejano)

    private Vector3 initialPosition;

    void Start()
    {
        // Guardar la posición inicial
        initialPosition = transform.position;
    }

    void Update()
    {
        // Mover el fondo más lentamente dependiendo de su parallaxFactor
        Vector3 delta = cameraTransform.position * parallaxFactor;
        transform.position = new Vector3(initialPosition.x + delta.x, initialPosition.y, initialPosition.z);
    }
}
