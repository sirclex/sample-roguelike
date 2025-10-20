using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Movement
    [HideInInspector]
    public float lastHorizontalVector;

    [HideInInspector]
    public float lastVerticalVector;

    [HideInInspector]
    public Vector2 direction;

    [HideInInspector]
    public Vector2 lastMovedVector;

    // Reference
    Rigidbody2D rb;
    PlayerStats player;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveX, moveY).normalized;

        if (direction.x != 0)
        {
            lastHorizontalVector = direction.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0);
        }

        if (direction.y != 0)
        {
            lastVerticalVector = direction.y;
            lastMovedVector = new Vector2(0, lastVerticalVector);
        }

        if (direction.x != 0 && direction.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(direction.x * player.currentMoveSpeed, direction.y * player.currentMoveSpeed);
    }
}
