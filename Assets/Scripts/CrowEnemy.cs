using UnityEngine;

public class CrowEnemy : MonoBehaviour
{
    public float diveSpeed = 5f; // Velocidad de descenso en picada
    public float hoverSpeed = 2f; // Velocidad de movimiento oscilante
    public float hoverRange = 3f; // Rango de oscilación (de lado a lado)
    public float attackRange = 10f; // Distancia máxima desde el jugador para iniciar el ataque
    public Transform leftBoundary; // Límite izquierdo del movimiento
    public Transform rightBoundary; // Límite derecho del movimiento

    private Transform player; // Referencia al jugador
    private Vector3 startPosition; // Posición inicial del cuervo
    private bool isAttacking = false; // Si el cuervo está atacando
    private bool movingRight = true; // Controla la dirección del movimiento horizontal
    private bool playerPassedBoundary = false; // Indica si el jugador cruzó el límite izquierdo
    private Rigidbody2D rb2D;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Asegúrate de que el jugador tenga la etiqueta "Player"
        startPosition = transform.position; // Guardar posición inicial
        rb2D = GetComponent<Rigidbody2D>();

        // Configurar el Rigidbody2D
        rb2D.gravityScale = 0; // Sin gravedad por defecto para vuelo
        rb2D.freezeRotation = true; // Evitar rotación automática
    }

    void Update()
    {
        if (player != null)
        {
            // Verificar si el jugador cruzó el límite izquierdo
            if (!playerPassedBoundary && player.position.x > leftBoundary.position.x)
            {
                playerPassedBoundary = true; // Activar el comportamiento del cuervo
            }

            if (playerPassedBoundary)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (isAttacking)
                {
                    PursuePlayer();
                }
                else if (distanceToPlayer <= attackRange)
                {
                    StartAttack();
                }
                else
                {
                    HoverMovement(); // Continuar movimiento oscilante
                }
            }
        }
    }

    void HoverMovement()
    {
        // Movimiento de lado a lado entre los límites
        if (movingRight)
        {
            rb2D.linearVelocity = new Vector2(hoverSpeed, 0);

            if (transform.position.x >= rightBoundary.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            rb2D.linearVelocity = new Vector2(-hoverSpeed, 0);

            if (transform.position.x <= leftBoundary.position.x)
            {
                movingRight = true;
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
    }

    void PursuePlayer()
    {
        // Movimiento en dirección al jugador
        Vector2 direction = (player.position - transform.position).normalized;
        rb2D.linearVelocity = direction * diveSpeed;

        // Si el cuervo está por debajo del jugador, termina el ataque
        if (transform.position.y <= player.position.y - 1f)
        {
            EndAttack();
        }
    }

    void EndAttack()
    {
        isAttacking = false; // Termina el ataque
        rb2D.linearVelocity = Vector2.zero; // Detener movimiento
        HoverMovement(); // Regresa al movimiento oscilante
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Activar animación de muerte del jugador
            Animator playerAnimator = collision.gameObject.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("DeathCrow");
            }

            // Opcional: Implementar lógica para reiniciar el nivel o manejar el evento de muerte
            Debug.Log("El cuervo atacó al jugador");
            EndAttack();
        }
    }
}
