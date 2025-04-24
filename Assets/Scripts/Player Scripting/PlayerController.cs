using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public Sprite[] sprites;  // Holds all 16 sprite frames
    private SpriteRenderer spriteRenderer;
    private int frameIndex = 0;
    private float frameRate = 0.1f;  // Time per frame
    private float frameTimer = 0f;
    private Vector2 movement;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Get movement input (WASD or Arrow Keys)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Prioritize horizontal movement over vertical (prevents diagonal speed boost)
        if (movement.x != 0) movement.y = 0;

        // Animate if moving
        if (movement != Vector2.zero)
        {
            frameTimer += Time.deltaTime;
            if (frameTimer >= frameRate)
            {
                frameTimer = 0f;
                frameIndex = (frameIndex + 1) % 4;  // Loop through 4 frames
                UpdateSprite();
            }
        }
        else
        {
            frameIndex = 0;  // Reset to first frame when idle
            UpdateSprite();
        }
    }

    void FixedUpdate()
    {
        // Move the player
        transform.position += new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.fixedDeltaTime;
    }

    void UpdateSprite()
    {
        if (movement.y < 0) spriteRenderer.sprite = sprites[frameIndex];       // Walk Down (Row 1)
        if (movement.x > 0) spriteRenderer.sprite = sprites[frameIndex + 4];  // Walk Right (Row 2)
        if (movement.y > 0) spriteRenderer.sprite = sprites[frameIndex + 8];  // Walk Up (Row 3)
        if (movement.x < 0) spriteRenderer.sprite = sprites[frameIndex + 12]; // Walk Left (Row 4)
    }
}
