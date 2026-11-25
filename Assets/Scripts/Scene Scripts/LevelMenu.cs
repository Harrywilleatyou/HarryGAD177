using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class LevelMenu : MonoBehaviour
{
    public GameObject levelMenu;
    public int weaponSlotOne;
    public int weaponSlotTwo;
    public int weaponSlotThree;
    public PlayerWeapons playerWeapons;

    [Header("Item Enhanceability")]
    public int fistEnhanceability = 1;
    public int bowEnhanceability = 1;
    public int tomeEnhanceability = 1;
    public int daggerEnhanceability = 1;

    void Start()
    {
        levelMenu.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevelMenu()
    {
        levelMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseLevelMenu()
    {
        levelMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GetObtainedItems()
    {
        if (playerWeapons.fistWeapon > 0)
        {
            if (playerWeapons.fistWeapon < 8)
                fistEnhanceability = 3;
            else
                fistEnhanceability = 0;
        }
        else fistEnhanceability = 1;

        if (playerWeapons.bowWeapon > 0)
        {
            if (playerWeapons.bowWeapon < 8)
                bowEnhanceability = 3;
            else
                bowEnhanceability = 0;
        }
        else bowEnhanceability = 1;

        if (playerWeapons.tomeWeapon > 0)
        {
            if (playerWeapons.tomeWeapon < 8)
                tomeEnhanceability = 3;
            else
                tomeEnhanceability = 0;
        }
        else tomeEnhanceability = 1;

        if (playerWeapons.daggerWeapon > 0)
        {
            if (playerWeapons.daggerWeapon < 8)
                daggerEnhanceability = 3;
            else
                daggerEnhanceability = 0;
        }
        else daggerEnhanceability = 1;

    }
    public void GenerateLevelOptions()
    {
        // TODO: Add script to roll for items to display for upgrade ignoring items with a level of 8. Already obtained items have double weight.
        Random.Range(0, 5);
    }
}
