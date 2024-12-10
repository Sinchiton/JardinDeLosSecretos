using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El jugador (Dalia)
    public Vector3 offset = new Vector3(0, 0, -10); // Ajuste opcional de la cámara
    public float smoothSpeed = 0.125f; // Velocidad de interpolación

    [Header("Zoom Settings")]
    public float zoomLevel = 5f; // Nivel de zoom deseado
    public float zoomSpeed = 2f; // Velocidad del ajuste de zoom

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (cam == null)
        {
            Debug.LogError("No se encontró la cámara principal.");
            return;
        }

        cam.orthographic = true; // Asegurar modo ortográfico
        cam.orthographicSize = zoomLevel; // Configurar nivel inicial de zoom
    }

    void LateUpdate()
    {
        if (target != null)
        {
            FollowPlayer();
            AdjustZoom();
        }
    }

    void FollowPlayer()
    {
        Vector3 desiredPosition = target.position + offset; // Calcular posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void AdjustZoom()
    {
        if (cam != null)
        {
            // Ajustar gradualmente el tamaño ortográfico hacia el nivel deseado
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, zoomLevel, Time.deltaTime * zoomSpeed);
        }
    }
}
