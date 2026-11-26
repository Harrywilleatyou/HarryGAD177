using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
public class LevelMenu : MonoBehaviour
{
    public GameObject levelMenu;
    public string option1;
    public string option2;
    public string option3;
    public PlayerWeapons playerWeapons;
    public PlayerExperience playerExperience;
    public int maxWeight;
    public int itemRoll;

    [Header("Item Enhanceability")]
    public int fistEnhanceability = 1;
    public int bowEnhanceability = 1;
    public int tomeEnhanceability = 1;
    public int daggerEnhanceability = 1;

    public string buttonText1;
    public string buttonText2;
    public string buttonText3;

    public TextMeshProUGUI buttonTMP1;
    public TextMeshProUGUI buttonTMP2;
    public TextMeshProUGUI buttonTMP3;

    [SerializeField] private List<string> weapons = new List<string>();

    void Start()
    {
        playerWeapons = FindAnyObjectByType<PlayerWeapons>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        RegenerateChoices();
    }

    void RegenerateChoices()
    {
        weapons.Clear();
        weapons.Add("Fists");
        weapons.Add("Bow and Arrow");
        weapons.Add("Unholy Tome");
        weapons.Add("Sacrifical Dagger");

        buttonText1 = GetObtainedItems();
        buttonText2 = GetObtainedItems();
        buttonText3 = GetObtainedItems();

        buttonTMP1.text = buttonText1;
        buttonTMP2.text = buttonText2;
        buttonTMP3.text = buttonText3;
    }

    public void OpenLevelMenu()
    {
        Time.timeScale = 0f;
    }

    public void CloseLevelMenu()
    {
        
        Time.timeScale = 1f;
        
        this.gameObject.SetActive(false);

    }

    public string GetObtainedItems()
    {
        int tempRandom = Random.Range(0, weapons.Count);
        string result = weapons[tempRandom];
        weapons.Remove(weapons[tempRandom]);
        return result;
    }
    public void GenerateLevelOptions()
    {
        // TODO: Make a list of items with an assigned result each and roll. Add the rolled item to first slot then remove from list and roll for next. After all 3 rolls add back items that have a level lower than 8.
        return;
        
    }

    public void LevelOptionOne()
    {
        playerWeapons.ItemLevelUp(buttonText1);
        CloseLevelMenu();
    }
    public void LevelOptionTwo()
    {
        playerWeapons.ItemLevelUp(buttonText2);
        CloseLevelMenu();
    }
    public void LevelOptionThree()
    {
        playerWeapons.ItemLevelUp(buttonText3);
        CloseLevelMenu();
    }
}
