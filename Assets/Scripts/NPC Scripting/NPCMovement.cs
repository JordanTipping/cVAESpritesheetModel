using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class NPCMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int frameIndex = 0;
    private float frameRate = 0.1f;
    private float frameTimer = 0f;
    private Vector2 movement;

    private float directionChangeTime = 0f;
    private float directionTimer = 0f;

    private bool bouncing = false; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        PickRandomDirection();
    }

    void Update()
    {
        directionTimer += Time.deltaTime;

        if (!bouncing && directionTimer >= directionChangeTime)
        {
            PickRandomDirection();
        }

        if (movement != Vector2.zero)
        {
            frameTimer += Time.deltaTime;
            if (frameTimer >= frameRate)
            {
                frameTimer = 0f;
                frameIndex = (frameIndex + 1) % 4;
                UpdateNPCSprite();
            }
        }
        else
        {
            frameIndex = 0;
            UpdateNPCSprite();
        }
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.fixedDeltaTime;
    }

    void PickRandomDirection()
    {
        directionTimer = 0f;
        directionChangeTime = Random.Range(1.0f, 4.0f);

        int direction = Random.Range(0, 5);

        switch (direction)
        {
            case 0: movement = Vector2.zero; break;
            case 1: movement = Vector2.up; break;
            case 2: movement = Vector2.right; break;
            case 3: movement = Vector2.down; break;
            case 4: movement = Vector2.left; break;
        }
    }

    void UpdateNPCSprite()
    {
        if (movement.y < 0) spriteRenderer.sprite = sprites[frameIndex];
        else if (movement.x > 0) spriteRenderer.sprite = sprites[frameIndex + 4];
        else if (movement.y > 0) spriteRenderer.sprite = sprites[frameIndex + 8];
        else if (movement.x < 0) spriteRenderer.sprite = sprites[frameIndex + 12];
        else spriteRenderer.sprite = sprites[0];
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bouncing) return;

        StartCoroutine(BounceBackThenReset());
    }

    IEnumerator BounceBackThenReset()
    {
        bouncing = true;

      
        movement = -movement;

        
        yield return new WaitForSeconds(0.5f);

        bouncing = false;
        PickRandomDirection(); 
    }
}
