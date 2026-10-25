using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(Rigidbody2D))]
public class TaliyahMovement : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        var keyboard = Keyboard.current;

        if (keyboard == null)
        {
            movement = Vector2.zero;
            return;
        }

        float x = (keyboard.dKey.isPressed ? 1 : 0)
                - (keyboard.aKey.isPressed ? 1 : 0);

        float y = (keyboard.wKey.isPressed ? 1 : 0)
                - (keyboard.sKey.isPressed ? 1 : 0);

        movement = new Vector2(x, y).normalized;

#elif ENABLE_LEGACY_INPUT_MANAGER
        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
#endif
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }
}