using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    public float moveSpeed = 2f;

    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
    }
}
