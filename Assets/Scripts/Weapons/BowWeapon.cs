using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class BowWeapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("References")]
    public GameObject arrowPrefab;
    public SpriteRenderer playerSpriteRenderer;
    public PlayerWeapons playerweapons;
    public EnemySpawner enemySpawner;
    public GameObject bestTarget;

    public float detectionRadius = 30f;
    public LayerMask enemyLayer;

    [Header("Animation")]
    public Animator animator;
    public bool useAttackAnimation = true;
    public string attackTriggerName = "Attack";

    public AudioSource audioSource;
    public AudioClip bowShoot;
    public bool shootSound = true;
    public float audioSpeed;

    [Header("Attack Settings")]
    public bool attacksEnabled = true;
    public int damage = 5;
    public float cooldown = 2.5f;
    public int arrowDelay = 2000;
    public int pierce = 1;
    public int velocity = 20;
    public Vector2 arrowDirection;
    public float maxSpeed = 0.75f;
    public float minSpeed = 3.5f;

    [Header("Slash Position")]
    public float slashOffsetX = 0f;
    public float slashOffsetY = 1f;

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

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }


        if (showDebugLogs == true)
        {
            Debug.Log("BowAttack: Started. Damage = " + damage + ", Cooldown = " + cooldown);
        }

        if (playerweapons == null)
        {
            playerweapons = gameObject.GetComponent<PlayerWeapons>();

        }

        if (enemySpawner == null)
        {
            enemySpawner = GameObject.FindFirstObjectByType<EnemySpawner>();
        }


    }

    private void Update()
    {
        if (playerweapons.bowWeaponLevel == 0)
        {
            return;
        }
        
        UpdateAutoAttack();
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
            if (cooldown < minSpeed && cooldown > maxSpeed)
                attackTimer = cooldown;
            else if (cooldown > minSpeed)
                attackTimer = minSpeed;
            else if (cooldown < maxSpeed)
                attackTimer = maxSpeed;
            else
            {
                attackTimer = cooldown;
                Debug.LogWarning("Bow Attack speed somehow out of range");
            }

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
                Debug.LogWarning("FistAttack: No Animator found.");
            }

            return;
        }

        // Play the attack animation.
        animator.SetTrigger(attackTriggerName);

        if (showDebugLogs == true)
        {
            Debug.Log("FistAttack: Attack animation triggered.");
        }
    }

    async void PerformSlash()
    { 
        if (arrowPrefab == null)
        {
            if (showDebugLogs == true)
            {
                Debug.LogWarning("BowAttack: No arrowPrefab assigned.");
            }

            return;
        }



        GameObject bestTarget = GetClosestEnemy();
            
        if (bestTarget == null)
            return;

        PlayAudio();
        arrowDelay = (int)(cooldown * 800);
        await Task.Delay(arrowDelay);

        if (bestTarget == null)
            bestTarget = GetClosestEnemy();

        Vector2 point = bestTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(point.y, point.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        float direction = 1f;

        Vector3 localSpawnPosition = new Vector3(slashOffsetX * direction, slashOffsetY, 0f);

        // Create the slash as a child of the player.
        GameObject slashObj = Instantiate(arrowPrefab, transform);

        slashObj.transform.localPosition = localSpawnPosition;
        slashObj.transform.localRotation = targetRotation;

        
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
            Debug.Log("SingleMeleeAutoAttack: Auto slash spawned. Damage: " + damage);
        }
    }

        
    public GameObject GetClosestEnemy()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Loop through the spawnedEnemies list on your EnemySpawner script
        foreach (GameObject enemyObject in enemySpawner.spawnedEnemies)
        {
            // Safety check in case an enemy was destroyed without being removed from the list
            if (enemyObject == null) continue;

            Vector3 directionToTarget = enemyObject.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = enemyObject;
            }
        }

        return bestTarget;
    }

    public void PlayAudio()
    {
        if (audioSource != null && shootSound == true)
        {
            float t = Mathf.InverseLerp(0.75f, 2.5f, cooldown);
            audioSource.pitch = Mathf.Lerp(3.34f, 1f, t);
            
            audioSource.PlayOneShot(bowShoot);
        }
    }

    public void ItemBowBoost(int level)
    {
        switch (level)
        {
            case 1:
                {
                    break;
                }


            case 2:
                {
                    damage += 2;
                    cooldown -= 0.3f;
                    break;
                }
            case 3:
                {
                    damage += 3;
                    pierce++;
                    break;
                }
            case 4:
                {
                    cooldown -= 0.3f;
                    velocity += 5;
                    break;
                }
            case 5:
                {
                    damage += 4;
                    break;
                }
            case 6:
                {
                    pierce++;
                    damage += 4;
                    break;
                }
            case 7:
                {
                    cooldown -= 0.4f;
                    break;
                }
            case 8:
                {
                    damage += 5;
                    pierce++;
                    velocity += 5;
                    break;
                }
        }
    }

}

