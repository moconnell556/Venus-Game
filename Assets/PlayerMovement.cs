using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float radius = 0.5f;   
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CalculateBounds();
    }

    void CalculateBounds()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        Vector2 center = cam.transform.position;

        minBounds = new Vector2(center.x - halfWidth + radius, center.y - halfHeight + radius);
        maxBounds = new Vector2(center.x + halfWidth - radius, center.y + halfHeight - radius);
    }

    void Update()
    {
        movementInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) movementInput.x -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) movementInput.x += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) movementInput.y -= 1;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) movementInput.y += 1;
        }

        if (movementInput.sqrMagnitude > 0)
        {
            movementInput.Normalize();
        }
    }

    void FixedUpdate()
    {
        Vector2 next = rb.position + movementInput * speed * Time.fixedDeltaTime;

        next.x = Mathf.Clamp(next.x, minBounds.x, maxBounds.x);
        next.y = Mathf.Clamp(next.y, minBounds.y, maxBounds.y);

        rb.MovePosition(next);
    }
}