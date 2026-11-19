using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float timePassed = 0f;
    [Header("Enemy")]
    public float healthScale = 1f;
    public float xpScale = 1f;

    public bool showTime = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();  

        int minutes = Mathf.FloorToInt(timePassed / 60);
        int seconds = Mathf.FloorToInt(timePassed % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateTime()
    {
        if (Time.timeScale > 0)
            timePassed += Time.deltaTime;
    }

    public void difficultyScale()
    {
        // if (enemy != null)
        
            
        
    }
}
