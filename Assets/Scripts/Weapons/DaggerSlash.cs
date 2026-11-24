using UnityEngine;

public class DaggerSlash : MonoBehaviour
{
    public DaggerWeapon daggerWeapon;

    [Header("Arrow Settings")]
    public float lifetime = 1f;
    public int damage = 5;
    public int velocity = 10;
    public int pierce = 0;
    public bool facingRight = true;

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
        if (daggerWeapon == null)
        {
            daggerWeapon = GameObject.FindAnyObjectByType<DaggerWeapon>();
        }
        velocity = daggerWeapon.velocity;
        pierce = daggerWeapon.pierce;
        transform.SetParent(null);
        facingRight = daggerWeapon.facingRight;
    }

    public void Initialize(int damageAmount)
    {
        // Set damage to the damageAmount passed into this function.
        damage = damageAmount;


    }
    private void Update()
    {
        if (Time.timeScale != 0f)
        {
            if (facingRight == true)
                transform.Translate(Vector2.right * velocity * Time.deltaTime);
            else
                transform.Translate(Vector2.left * velocity * Time.deltaTime);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Try to get the EnemyHealth component from the object that entered the trigger.
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        // TODO: Check that enemy is not null.
        if (enemy != null)
        {
            // TODO: Call the enemy's TakeDamage function and give it this slash's damage amount.
            if (enemy.currentHP <= damage)
            {
                daggerWeapon.killCount++;
                daggerWeapon.DamageScale();
                if (showDebugLogs == true)
                    Debug.Log("Kill count increased to " + daggerWeapon.killCount);
            }
            enemy.TakeDamage(damage);
            pierce -= 1;
            // TODO: If debug logs are enabled, print the enemy hit and damage dealt.
            if (showDebugLogs == true)
            {
                Debug.Log(damage + " damage dealt to " + enemy);
            }

            

            if (pierce < 0)
            {
                Destroy(gameObject);
            }

        }
    }
}
