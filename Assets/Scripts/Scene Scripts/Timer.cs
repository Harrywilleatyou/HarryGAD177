using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float timePassed = 0f;
    private float difficultyCooldown = 30f;
    private float cooldownTimer = 0f;

    [Header("Enemy")]
    public EnemySpawner enemySpawner;
    public int spawnedEnemy = 1;
    public float scaleMult = 1f;

    [Header("Debug")]
    public bool enableDebugKeys = false;
    public bool showDebugLogs = false;

    public Key debugTimeForwardKey = Key.Space;
    public int debugSeconds = 60;

    public bool showTime = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (enemySpawner == null)
            enemySpawner = GameObject.FindAnyObjectByType<EnemySpawner>();
        cooldownTimer = difficultyCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        DifficultyScale();
        HandleDebugInput();

        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timePassed > 120 && timePassed < 240)
        {
            enemySpawner.enemyNumber = 1;
        }
    }

    public void UpdateTime()
    {
        if (Time.timeScale > 0)
            timePassed += Time.deltaTime;
    }

    public void DifficultyScale()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            cooldownTimer = difficultyCooldown;
            scaleMult += 0.1f;
            enemySpawner.spawnRateMult += 0.1f;
            enemySpawner.SpecialSpawnEnemy();
        }
        

    }

    public void HandleDebugInput()
    {
        if (enableDebugKeys == false)
        {
           
            return;
        }

        if (Keyboard.current == null)
        {
            
            return;
        }
        
        // If the debug key is pressed,
        // Skip time forward by debugSeconds
        if (Keyboard.current[debugTimeForwardKey].wasPressedThisFrame)
        {
            timePassed += debugSeconds;
            if (showDebugLogs == true)
            Debug.Log("time skipped by " + debugSeconds + " seconds");
        }
    }
}
