using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;

    private Rigidbody2D rb;
    private Vector2 movement;

    private float screenHalfWidth;
    private float screenHalfHeight;

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
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -screenHalfWidth + 0.5f, screenHalfWidth - 0.5f);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenHalfHeight + 0.5f, screenHalfHeight - 0.5f);

        rb.MovePosition(newPosition);
    }

}
