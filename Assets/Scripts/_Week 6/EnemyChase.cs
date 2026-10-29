using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyChase : MonoBehaviour
{
    // Complete each section marked TODO.

    [Header("Movement")]
    public float moveSpeed = 2f;

    [Header("Target")]
    public Transform target;

    private Rigidbody2D playerbody; 

    [Header("Animation")]
    public Animator animator;
    public string isMovingParameter = "isMoving";

    [Header("Sprite")]
    public SpriteRenderer spriteRenderer;
    public bool faceRightByDefault = true;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        // TODO: Get the Rigidbody2D component from this GameObject.

        rb = GetComponent<Rigidbody2D>();

        // TODO: Check if animator is null.
        // If it is, get the Animator component from this GameObject.

        if (animator = null)
            {
                GetComponent<Animator>();
            }

        // TODO: Check if spriteRenderer is null.
        // If it is, get the SpriteRenderer from this GameObject or its children.
        if (spriteRenderer = null)
        {
            GetComponent<SpriteRenderer>();
        }

    }

    private void Start()
    {
        // Check if a target has not been assigned.
        if (target == null)
        {
            // TODO: Find the GameObject with the Player tag.
            GameObject playerObject = GameObject.FindAnyObjectByType<PlayerHealth>().gameObject;

            // TODO: Check that playerObject is not null.
            // If it exists, set target to the player's Transform.
            if (playerObject != null )
            {
                target = playerObject.GetComponent<Transform>();
            }
            playerbody = playerObject.GetComponent<Rigidbody2D>();
        }
    }

    private void FixedUpdate()
    {
        // TODO: Check if target is null.
        // If it is, stop the enemy moving and stop this function.

        if (target == null)
        {
            moveSpeed = 0f;
            return;
        }

        // TODO: Calculate the direction from the enemy to the target.
        // Normalise the direction and store it in moveDirection.
       
        

        Vector2 moveDirection = (playerbody.position - (Vector2)transform.position).normalized;


        // TODO: Move the enemy using moveDirection and moveSpeed.
        rb.linearVelocity = moveDirection * moveSpeed;

        // TODO: Call the function that updates the animation.

        UpdateAnimation();  

        // TODO: Call the function that updates the sprite direction.
        UpdateSpriteFlip();

    }

    private void UpdateAnimation()
    {
        if (animator == null)
        {
            return;
        }

        // TODO: Create a bool that checks whether the enemy is moving.
        bool isMoving = false;

        // TODO: Update the Animator's isMoving parameter.

        if (moveSpeed != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    private void UpdateSpriteFlip()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        // Only change direction when moving left or right.
        if (moveDirection.x > 0f)
        {
            // TODO: Set flipX so the enemy faces right.
            spriteRenderer.flipX = !faceRightByDefault;

        }
        else if (moveDirection.x < 0f)
        {
            // TODO: Set flipX so the enemy faces left.
            spriteRenderer.flipX = faceRightByDefault;

        }
    }
}

