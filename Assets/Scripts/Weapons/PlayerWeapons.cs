using UnityEngine;
using UnityEngine.Playables;

public class PlayerWeapons : MonoBehaviour
{
    public int fistWeaponLevel = 1;
    public int bowWeaponLevel = 0;
    public int tomeWeaponLevel = 0;
    public int daggerWeaponLevel = 0;

    [Header("References")]
    public FistWeapon fistWeapon;
    public TomeWeapon tomeWeapon;
    public BowWeapon bowWeapon;
    public DaggerWeapon daggerWeapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Dookie()
    {
        Debug.Log("samta");
    }


    public void ItemLevelUp(string option)
    {
        switch (option)
        {
            case "Fists":
                {
                    fistWeaponLevel++;
                    fistWeapon.ItemFistBoost(fistWeaponLevel);
                    break;
                }

            case "Bow and Arrow":
                {
                    bowWeaponLevel++;
                    bowWeapon.ItemBowBoost(bowWeaponLevel);
                    break;
                }

            case "Unholy Tome":
                {
                    tomeWeaponLevel++;
                    tomeWeapon.ItemTomeBoost(tomeWeaponLevel);
                    break;
                }
            case "Sacrifical Dagger":
                {
                    daggerWeaponLevel++;
                    daggerWeapon.ItemDaggerBoost(daggerWeaponLevel);
                    break;
                }

            default: // Runs if no other case matches
                Debug.LogWarning("Invalid item leveled.");
                break;
        }
    }

    
        
}
