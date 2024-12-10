using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 2f; // Velocidad de caminar
    public float runMultiplier = 1.5f; // Multiplicador de correr
    public float jumpSpeed = 5f; // Velocidad de salto
    public Transform groundCheck; // Transform para verificar el suelo
    public float groundCheckRadius = 0.2f; // Radio del GroundCheck
    public LayerMask groundLayer; // Capa considerada como suelo
    public LayerMask deathLayer; // Capa que causa muerte

    private Rigidbody2D rb2D;
    private Animator animator; // Referencia al Animator
    private bool isGrounded; // Verifica si el jugador está en el suelo
    private bool isDead; // Verifica si el jugador está muerto
    private GameUIManager uiManager; // Referencia al GameUIManager
    private bool facingRight = true; // Indica si el jugador está mirando a la derecha

    void OnEnable()
    {
        InitializeReferences(); // Asegurarse de que las referencias estén configuradas
        ResetPlayer(); // Resetear el estado del jugador
    }

    void Start()
    {
        InitializeReferences(); // Asegurarse de que las referencias estén configuradas al iniciar
    }

    void InitializeReferences()
    {
        if (rb2D == null) rb2D = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (uiManager == null) uiManager = FindObjectOfType<GameUIManager>();

        rb2D.freezeRotation = true; // Evitar que el Rigidbody rote
    }

    void Update()
    {
        if (!isDead)
        {
            // Verificar si el jugador está tocando el suelo
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            animator.SetBool("IsGrounded", isGrounded); // Actualizar el estado del Animator

            MovePlayer();
            Jump();
        }
    }

    void MovePlayer()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed * runMultiplier : walkSpeed;

        // Movimiento horizontal
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb2D.linearVelocity = new Vector2(moveHorizontal * moveSpeed, rb2D.linearVelocity.y);

        // Actualizar animaciones
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal)); // Cambia la animación según la velocidad

        // Voltear al jugador según la dirección
        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        // Salto solo si está en el suelo
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpSpeed);
            animator.SetTrigger("Jump"); // Activar animación de salto
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador colisiona con un objeto en la capa DeathLayer
        if (((1 << collision.gameObject.layer) & deathLayer) != 0)
        {
            Die();
        }
    }

    public void Die()
{
    isDead = true;
    rb2D.linearVelocity = Vector2.zero;
    animator.SetTrigger("Death");

    // Llamar al GameOverManager
    GameOverManager gameOverManager = FindObjectOfType<GameOverManager>();
    if (gameOverManager != null)
    {
        gameOverManager.TriggerGameOver();
    }
}


    void ResetPlayer()
    {
        isDead = false; // Marcar al jugador como vivo
        rb2D.linearVelocity = Vector2.zero; // Detener movimiento
        if (animator != null) animator.SetBool("IsGrounded", true); // Reiniciar animación
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Cambiar la dirección en el eje X
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
