using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject[] clouds; // Arreglo de nubes (agregar manualmente las nubes en el Inspector).
    public float speed = 2f; // Velocidad de movimiento hacia la izquierda.
    public float offScreenPositionX = -20f; // Posición X donde la nube desaparece (fuera de la pantalla a la izquierda).
    public float resetPositionX = 20f; // Posición X donde la nube reaparece (fuera de la pantalla a la derecha).

    void Update()
    {
        foreach (GameObject cloud in clouds)
        {
            // Mover cada nube hacia la izquierda.
            cloud.transform.position += Vector3.left * speed * Time.deltaTime;

            // Verificar si la nube salió de la pantalla por completo.
            if (cloud.transform.position.x <= offScreenPositionX)
            {
                // Reposicionar la nube al lado derecho.
                Vector3 newPosition = cloud.transform.position;
                newPosition.x = resetPositionX;
                cloud.transform.position = newPosition;
            }
        }
    }
}
