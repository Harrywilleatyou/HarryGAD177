using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowShoot : MonoBehaviour
{
    public BowWeapon bowWeapon;

    [Header("Arrow Settings")]
    public float lifetime = 1f;
    public int damage = 5;
    public int velocity = 20;
    public int pierce = 1;

    private Vector2 moveDirection;

    [Header("Debug")]
    public bool showDebugLogs = false;

    private void Awake()
    {
        //Get the Collider2D component from this GameObject.
        Collider2D col = GetComponent<Collider2D>();

        // Check that col is not null.
        // If it exists, set it to be a trigger.
        if (col != null)
        {
            col.isTrigger = true;
        }

    }

    private void Start()
    {
        // Remove the projectile after its lifetime has passed.
        Destroy(gameObject, lifetime);
        if (bowWeapon == null)
        {
            bowWeapon = GameObject.FindAnyObjectByType<BowWeapon>();
        }
        velocity = bowWeapon.velocity;
        pierce = bowWeapon.pierce;
        moveDirection = bowWeapon.arrowDirection;
        transform.SetParent(null);
    }

    public void Initialize(int damageAmount)
    {
        // Set damage to the damageAmount passed into this function.
        damage = damageAmount;
        

    }
    private void Update()
    {
        if (Time.timeScale != 0f)
            transform.Translate(Vector2.right * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Try to get the EnemyHealth component from the object that entered the trigger.
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        // TODO: Check that enemy is not null.
        if (enemy != null)
        {
            // TODO: Call the enemy's TakeDamage function and give it this slash's damage amount.
            enemy.TakeDamage(damage);
            pierce -= 1;
            // TODO: If debug logs are enabled, print the enemy hit and damage dealt.
            if (showDebugLogs == true)
            {
                Debug.Log(damage + "damage dealt to" + enemy);
            }

            if (pierce < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
