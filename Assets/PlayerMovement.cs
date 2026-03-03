using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Attack Settings")]
    public bool isAttacking = false;
    public float attackDuration = 0.3f;
    private float attackTimer = 0f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Joystick joystick;

    private Rigidbody2D rb;
    private Vector2 movement;

    private float screenHalfWidth;
    private float screenHalfHeight;

    [Header("Player Stats")]
    public int playerHealth = 3; // number of lives

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        screenHalfHeight = Camera.main.orthographicSize;
        screenHalfWidth = screenHalfHeight * Screen.width / Screen.height;
    }

    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // Update attack timer
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                isAttacking = false;
            }
        }
    }

    void FixedUpdate()
    {
        // Move player
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        // Keep player inside screen bounds
        newPosition.x = Mathf.Clamp(newPosition.x, -screenHalfWidth + 0.5f, screenHalfWidth - 0.5f);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenHalfHeight + 0.5f, screenHalfHeight - 0.5f);

        rb.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            FindFirstObjectByType<GameManager>().AddScore();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                Destroy(other.gameObject);
            }
            else
            {
                // Player takes damage
                playerHealth--;

                if (playerHealth > 0)
                {
                    Debug.Log("Player hit! Lives left: " + playerHealth);
                }
                else
                {
                    Debug.Log("Player Dead! Game Over");
                    // Call GameManager to show Game Over UI
                    FindFirstObjectByType<GameManager>().GameOver();
                }
            }
        }
    }

    // Called when Attack button is pressed
    public void Attack()
    {
        isAttacking = true;
        attackTimer = attackDuration;
    }
}
