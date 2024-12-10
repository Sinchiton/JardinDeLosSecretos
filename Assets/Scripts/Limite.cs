using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Warrior"
        if (collision.gameObject.CompareTag("Warrior"))
        {
            // Obt�n el componente Rigidbody2D del personaje
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            // Det�n el movimiento del personaje
            rb.linearVelocity = Vector2.zero;

            // Opcional: Ajusta la posici�n para que no se salga del mapa
            Vector3 currentPosition = collision.gameObject.transform.position;
            collision.gameObject.transform.position = new Vector3(currentPosition.x, Mathf.Max(currentPosition.y, 0), currentPosition.z);

            // Opcional: Mostrar mensaje en consola
            Debug.Log("L�mite alcanzado. Movimiento detenido.");
        }
    }
}
