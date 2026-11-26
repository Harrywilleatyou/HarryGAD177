using Unity.VisualScripting;
using UnityEngine;

public class TomeAura : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackTimer = 0f;
    public float cooldown = 1;
    public int damage;
    public float radius = 1.6f;

    [Header("References")]
    public TomeWeapon tomeWeapon;
    public PlayerWeapons playerweapons;
    public LayerMask enemyLayer;

    public bool showDebugLogs = false;
    public float pulseSpeed = 3f;
    public float pulseAmount = 0.05f;
    public float rotationSpeed = 40f;
    private Vector3 initialScale;
    private Vector3 mainScale;
    public float scaleMult = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (tomeWeapon == null)
            tomeWeapon = GameObject.FindAnyObjectByType<TomeWeapon>();
        if (showDebugLogs == true)
            Debug.Log("GameObject found " + tomeWeapon);
        if (playerweapons == null)
        {
            playerweapons = GameObject.FindAnyObjectByType<PlayerWeapons>();
            if (showDebugLogs == true)
                Debug.Log("Game Object found " + playerweapons);
        }
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerweapons.tomeWeaponLevel > 0)
        {
            if (attackTimer <= 0f)
            {
                TriggerDamage();
                attackTimer = cooldown;

            }
            scaleMult = tomeWeapon.auraScale;
            
            attackTimer -= Time.deltaTime;

            mainScale = initialScale * scaleMult;
            // 1. Continuous, frame-rate independent rotation
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

            // 2. Smooth sine-wave pulsing
            float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            transform.localScale = mainScale + (mainScale * scaleOffset);
        }
    }

   
    public void TriggerDamage()
    {
        // 1. Find all colliders on the enemyLayer within the radius
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        // 2. Loop through every detected collider
        foreach (Collider2D hit in hitColliders)
        {
            // 3. Get the enemy script and call your function
            if (hit.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                damage = tomeWeapon.damage;
                enemy.TakeDamage(damage);
            }
        }
    }

}
