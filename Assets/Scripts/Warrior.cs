using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    public float fuerzaSalto = 10f; // Fuerza para el salto
    public float velocidadPersonaje = 5f; // Velocidad horizontal del personaje

    private Rigidbody2D rb2D;
    private Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Control de salto
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb2D.linearVelocity.y) < 0.01f)
        {
            animator.SetBool("estaSaltando", true);
            rb2D.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float velocidadInput = Input.GetAxis("Horizontal"); // Detecta las teclas "A/D" o las flechas izquierda/derecha
        rb2D.linearVelocity = new Vector2(velocidadInput * velocidadPersonaje, rb2D.linearVelocity.y);

        // Actualización del parámetro de animación
        animator.SetFloat("camina", Mathf.Abs(velocidadInput));

        // Cambiar dirección del sprite según el movimiento
        if (velocidadInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Voltear a la izquierda
        }
        else if (velocidadInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Voltear a la derecha
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            animator.SetBool("estaSaltando", false);
        }
    }
}
