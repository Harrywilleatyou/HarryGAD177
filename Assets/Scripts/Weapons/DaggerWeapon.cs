using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


public class DaggerWeapon : MonoBehaviour
{
    [Header("References")]
    public GameObject slashPrefab;
    public SpriteRenderer playerSpriteRenderer;
    public PlayerWeapons playerweapons;


    [Header("Animation")]
    public Animator animator;
    public bool useAttackAnimation = true;
    public string attackTriggerName = "Attack";

    [Header("Facing")]
    public bool facingRight = true;
    public bool faceRightByDefault = true;

    [Header("Attack Settings")]
    public bool attacksEnabled = true;
    public int damage = 5;
    public float cooldown = 1.5f;
    public float maxSpeed = 0.45f;
    public float minSpeed = 2.5f;
    public int pierce = 0;
    public int velocity = 10;
    public int killCount = 0;
    public int killGoal = 10;

    [Header("Slash Position")]
    public float slashOffsetX = 0.5f;
    public float slashOffsetY = 0f;

    [Header("Slash Visual")]
    public bool flipSlashWhenFacingLeft = true;

    [Header("Debug")]
    public bool showDebugLogs = false;

    [Header("Debug View")]
    public float attackTimer = 0f;

    private void Start()
    {
        // Check if playerSpriteRenderer is null.
        // If it is, get the SpriteRenderer from this GameObject or its children.

        if (playerSpriteRenderer == null)
        {
            playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        // Get the Animator if one has not been assigned.
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        // Set the attack timer to the cooldown value.
        attackTimer = cooldown;


        if (showDebugLogs == true)
        {
            Debug.Log("FistAttack: Started. Damage = " + damage + ", Cooldown = " + cooldown);
        }

        if (playerweapons == null)
        {
            playerweapons = gameObject.GetComponent<PlayerWeapons>();

        }



    }

    private void Update()
    {
        if (playerweapons.daggerWeapon == 0)
        {
            return;
        }
        UpdateFacingFromSprite();
        UpdateAutoAttack();
    }

    private void UpdateFacingFromSprite()
    {
        if (playerSpriteRenderer == null)
        {
            return;
        }

        // Check whether the player's sprite faces right by default.
        if (faceRightByDefault == true)
        {
            // TODO: Set facingRight based on whether the SpriteRenderer is flipped
            if (playerSpriteRenderer.flipX == false)
            {
                facingRight = true;
            }


            // Set facingRight based on whether the SpriteRenderer is flipped.
            if (playerSpriteRenderer.flipX == true)
            {
                facingRight = false;
            }

        }
    }

    private void UpdateAutoAttack()
    {
        if (attacksEnabled == false)
        {
            return;
        }

        // Reduce the attack timer each frame.
        attackTimer -= Time.deltaTime;

        // When the timer reaches 0, attack and reset the timer.
        if (attackTimer <= 0f)
        {
            // Call the Attack function.
            Attack();


            // Reset attackTimer to the cooldown value.
            attackTimer = cooldown;

        }
    }

    private void Attack()
    {
        // Call the function that performs the melee slash.

        PerformSlash();

        // Call the function that plays the attack animation.

        PlayAttackAnimation();
    }

    private void PlayAttackAnimation()
    {
        if (useAttackAnimation == false)
        {
            return;
        }

        if (animator == null)
        {
            if (showDebugLogs == true)
            {
                Debug.LogWarning("DaggerAttack: No Animator found.");
            }

            return;
        }

        // Play the attack animation.
        animator.SetTrigger(attackTriggerName);

        if (showDebugLogs == true)
        {
            Debug.Log("DaggerAttack: Attack animation triggered.");
        }
    }
    public void DamageScale()
    {
        if (killCount >= killGoal)
        {
            killCount -= killGoal;
            killGoal += 10;
            damage += 1;
            if (showDebugLogs == true)
                Debug.Log("Damage increased to " + damage);
        }
    }

    private void PerformSlash()
    {
        if (slashPrefab == null)
        {
            if (showDebugLogs == true)
            {
                Debug.LogWarning("DaggerAttack: No slashPrefab assigned.");
            }

            return;
        }

        // Choose which direction the slash should appear.
        float direction = facingRight ? 1f : -1f;

        Vector3 localSpawnPosition = new Vector3(slashOffsetX * direction, slashOffsetY, 0f);

        // Create the slash as a child of the player.
        GameObject slashObj = Instantiate(slashPrefab, transform);

        slashObj.transform.localPosition = localSpawnPosition;
        slashObj.transform.localRotation = Quaternion.identity;

        if (flipSlashWhenFacingLeft == true)
        {
            // TODO: Get the SpriteRenderer from the slash object.

            SpriteRenderer slashRenderer = slashObj.GetComponentInChildren<SpriteRenderer>();

            // TODO: Check that slashRenderer is not null.
            // If it exists, set its flipX value based on facingRight.
            if (slashRenderer != null)
            {

                if (direction == 1f)
                {
                    slashRenderer.flipX = false;
                }
                else
                {
                    slashRenderer.flipX = true;
                }
            }
            else
            {
                Debug.Log("fucky wucky");
            }

        }

        // TODO: Get the MeleeSlash script from the slash object.
        MeleeSlash slash = slashObj.GetComponent<MeleeSlash>();

        // TODO: Check that slash is not null.
        // If it exists, call Initialize and give it the player's damage.
        if (slash != null)
        {
            slash.Initialize(damage);
        }

        if (showDebugLogs == true)
        {
            Debug.Log("SingleMeleeAutoAttack: Auto slash spawned. FacingRight = " + facingRight + ", Damage = " + damage);
        }
    }
}