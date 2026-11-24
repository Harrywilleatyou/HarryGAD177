using Unity.VisualScripting;
using UnityEngine;

public class TomeWeapon : MonoBehaviour
{
    [Header("References")]
    public GameObject tomeAuraPrefab;
    public SpriteRenderer playerSpriteRenderer;
    public PlayerWeapons playerweapons;
    public GameObject tomeAuraObj;


    [Header("Animation")]
    public Animator animator;
    public bool useAttackAnimation = true;
    public string attackTriggerName = "Attack";


    [Header("Attack Settings")]
    public bool attacksEnabled = true;
    public int damage = 4;
    public float cooldown = 1.5f;
    public float maxSpeed = 0.45f;
    public float minSpeed = 2.5f;


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
        if (playerweapons.tomeWeapon >= 1)
            TomeSpawn();


    }

    private void Update()
    {
        if (playerweapons.tomeWeapon <= 0)
        {
            return;
        }
    }
    public void TomeSpawn()
    {
        GameObject tomeAuraObj = Instantiate(tomeAuraPrefab, transform);
    }

}
