using UnityEngine;

public class ExperienceDrop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int expValue = 1;
    public bool showDebugLogs = true;

    private void Awake()
    {
        // TODO: Get the Collider2D component from this GameObject.
        Collider2D col = GetComponent<Collider2D>();



        // TODO: Check that col is not null.
        // If it exists, set the Collider2D to be a trigger.
        if (col != null)
        {
            col.isTrigger = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Check whether the object that entered the trigger
        // has the Player tag.
        // If it does not, stop this function.

        if (!other.CompareTag("Player"))
        {
            return;
        }

        // TODO: Get the PlayerMoney component from the object
        // that entered the trigger.
        PlayerExperience exp = other.GetComponent<PlayerExperience>();



        // TODO: If money is null, try to find the
        // PlayerMoney component in the scene.
        if (exp == null)
        {
            exp = GameObject.FindAnyObjectByType<PlayerExperience>();
        }


        // TODO: Check that money is not null.
        // If it exists:
        // - Add moneyAmount to the player's money.
        // - Print a debug message if debug logs are enabled.

        if (exp != null)
        {

            exp.AddXP(expValue);

            if (showDebugLogs == true)
            {
                Debug.Log("Exp Get");
            }
        }



        // Remove the coin after it has been collected.
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
