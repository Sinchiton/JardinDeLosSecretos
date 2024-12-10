using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform warrior; // Asigna el objeto del personaje en el Inspector
    [SerializeField] private float detectionRadius = 5f; // Radio de detecci�n del enemigo
    [SerializeField] private float speed = 2f; // Velocidad del enemigo
    [SerializeField] private float attackRange = 1.5f; // Distancia m�nima para atacar
    [SerializeField] private float attackCooldown = 1f; // Tiempo entre ataques

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private Animator animator;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown; // Permitir un ataque inmediato al inicio
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, warrior.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (warrior.position - transform.position).normalized;

            if (distanceToPlayer <= attackRange)
            {
                Attack(); // Atacar si est� dentro del rango
                movement = Vector2.zero; // Detener el movimiento mientras ataca
                enMovimiento = false;
            }
            else
            {
                movement = new Vector2(direction.x * speed, 0); // Movimiento horizontal hacia el jugador
                enMovimiento = true;

                // Cambiar la escala del enemigo seg�n la direcci�n
                if (direction.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1); // Mirar hacia la izquierda
                else if (direction.x > 0)
                    transform.localScale = new Vector3(1, 1, 1); // Mirar hacia la derecha
            }
        }
        else
        {
            movement = Vector2.zero; // Detener movimiento si est� fuera del rango
            enMovimiento = false;
        }

        animator.SetBool("enMovimiento", enMovimiento); // Actualizar el estado de animaci�n
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            animator.SetTrigger("Atacar"); // Reproducir animaci�n de ataque
            lastAttackTime = Time.time;

            // Opcional: Aplicar da�o al personaje aqu�
            Debug.Log("El enemigo ataca al jugador!");
        }
    }
}
