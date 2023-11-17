using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerStats : MonoBehaviour
{

    CharacterScripatableObject characterData;





    
    float currentHealth;

     float currentRecovery;

    float currentMoveSpeed;

     float currentMight;
    
    float currentProjectileSpeed;


    float currentMagnet;


    
    public float CurrentHealth
    {
        get { return currentHealth; } 
        set
        {
            if (currentHealth != value) 
            { 
                currentHealth = value;
                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text ="Health: "+ currentHealth;
                }
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
            {
                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }

    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value)
            {
                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Speed: " + currentMoveSpeed;
                }
            }
        }
    }

    public float CurrentMight 
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value)
            {
                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value)
            {
                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value)
            {
                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magent: " + currentMagnet;
                }
            }
        }
    }




     

    //Experience and level of the player 

    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;



    [System.Serializable]

    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }




    //I-frames 

    [Header("I-Frames")]
    public float invincibilityDurations;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;


    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;


    [Header("UI Health")]
    public Image healthbar;

    public Image ExpBar;
    public Text LevelText;

    private void Awake()
    {
        characterData = CharacterSelector.GetData();

        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();


        CurrentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.Projectilespeed;
        currentMagnet = characterData.Magnet;


        SpawnWeapon(characterData.Startingweapon);


    }

    private void Start()
    {
        //
        experienceCap = levelRanges[0].experienceCapIncrease;


        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text= "Magnet: "+ currentMagnet;


        GameManager.instance.AssignChosenCharacterUI(characterData);

        UpdateHealthBar();

        UpdateExpBar();

        UpdateLevelText();

    }


    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;

        }
        else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;



        LevelUpChecker();

        UpdateExpBar();
    }



    void LevelUpChecker()
    {

        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;

            UpdateLevelText();

            GameManager.instance.StartLevelUp();
        }
    }




    public void UpdateExpBar()
    {
        ExpBar.fillAmount = (float)experience / experienceCap;
    }

    public void UpdateLevelText()
    {
        LevelText.text = "LV " + level.ToString(); 
    }

    public void TakeDamage(float dmg)
    {






        if (!isInvincible)
        {
            CurrentHealth -= dmg;

            invincibilityTimer = invincibilityDurations;
            isInvincible = true;
            if (CurrentHealth < 0)
            {
                Kill();
            }


            UpdateHealthBar();
        }
    }


    public void UpdateHealthBar()
    {
        healthbar.fillAmount = currentHealth / characterData.MaxHealth;
    }



    public void Kill()
    {
        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignedChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.PassiveItemUISlots);
            GameManager.instance.GameOver();
        }
    }




    public void RestoreHealth(float amount)
    {

        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;

            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }



    }


    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;


            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }





    public void SpawnWeapon(GameObject weapon)
    {

        // check if slots are full 
        if (weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        //starting weapon

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);

        spawnedWeapon.transform.SetParent(transform);

        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());


        weaponIndex++;


    }


    public void SpawnPassiveItems(GameObject passiveItems
        )
    {

        // check if slots are full 
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        //starting weapon

        GameObject spawnedPassiveItem = Instantiate(passiveItems, transform.position, Quaternion.identity);

        spawnedPassiveItem.transform.SetParent(transform);

        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<passiveItem>());


        passiveItemIndex++;
    }
}
