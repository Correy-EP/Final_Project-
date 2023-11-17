using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class InventoryManager : MonoBehaviour
{
    public List<WeaponController> weaponSlots = new List<WeaponController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>();
    public List<passiveItem> passiveItemSlots = new List<passiveItem>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> PassiveItemUISlots = new List<Image>();

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex; 
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData; 
    }
    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public int passiveItemUpgradeIndex;
        public GameObject initialPassiveItem;
        public passiveItemScriptableObject PassiveItemData;
    }
    [System.Serializable]
    public class UpgradeUI
    {
        public Text upgradeNameDisplay;
        public Text UpgradeDescriptionDisplay;

        public Image upgradeIcon;

        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();


    PlayerStats player;

     void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    public void AddWeapon(int slotIndex, WeaponController weapon) // add a weapon to a slot 
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true; 
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;

        if (GameManager.instance != null && GameManager.instance.choosingUpgrade) 
        {
            GameManager.instance.EndLevelUp();
        }

    }

    public void AddPassiveItem(int slotIndex, passiveItem passiveItem) // add a passive to a slot 
    {
        passiveItemSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        PassiveItemUISlots[slotIndex].enabled =true; 
        PassiveItemUISlots[slotIndex]. sprite = passiveItem.passiveItemData.Icon;

        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        if(weaponSlots.Count > slotIndex)
        {
            WeaponController weapon = weaponSlots[slotIndex];

            if (!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LEVEL FOR " + weapon.name);
                return; 
            }
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab,transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<WeaponController>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<WeaponController>().weaponData.Level;

            weaponUpgradeOptions[upgradeIndex].weaponData = upgradedWeapon.GetComponent<WeaponController>().weaponData;

            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }

        }
    }

    public void LevelUpPassiveItem(int slotIndex, int upgradeIndex)
    {
        if (passiveItemSlots.Count > slotIndex)
        {
            passiveItem passiveItem = passiveItemSlots[slotIndex];
            if (!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.LogError("NO NEXT LEVEL FOR " + passiveItem.name);
                return;
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<passiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<passiveItem>().passiveItemData.Level;

            passiveItemUpgradeOptions[upgradeIndex].PassiveItemData = upgradedPassiveItem.GetComponent<passiveItem>().passiveItemData;

            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }

        }

    }

    void ApplyUpgradeOptions()
    {
        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<PassiveItemUpgrade>availablePassiveItemUpgrades = new List<PassiveItemUpgrade>(passiveItemUpgradeOptions);
        foreach(var upgradeOption in upgradeUIOptions)
        {
            if(availableWeaponUpgrades.Count == 0 && availablePassiveItemUpgrades.Count == 0)
            {
                return; 
            }

            int upgradeType;

            if(availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }

            else if (availablePassiveItemUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                {
                    upgradeType = Random.Range(1,3);
                }
            }

            if (upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0,availableWeaponUpgrades.Count)];

                availableWeaponUpgrades.Remove(chosenWeaponUpgrade); 

                if(chosenWeaponUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption); 

                    bool newWeapon = false;
                    for (int i =0; i< weaponSlots.Count; i++)
                    {
                        if (weaponSlots[i]!=null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;
                            if (!newWeapon)
                            {

                                if (!chosenWeaponUpgrade.weaponData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }

                                upgradeOption.upgradeButton.onClick.AddListener(()=> LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex));
                                upgradeOption.UpgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<WeaponController>().weaponData.Name;

                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    }
                    if (newWeapon)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(()=> player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));

                        upgradeOption.UpgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description; 
                        upgradeOption.upgradeNameDisplay.text=chosenWeaponUpgrade.weaponData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                    
                }
            }
            else if (upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade = availablePassiveItemUpgrades[Random.Range(0,availablePassiveItemUpgrades.Count)];

                availablePassiveItemUpgrades.Remove(chosenPassiveItemUpgrade);

                if(chosenPassiveItemUpgrade != null)
                {
                    EnableUpgradeUI(upgradeOption);

                    bool newPassiveItem = false; 
                    for( int i = 0; i< passiveItemSlots.Count; i++)
                    {
                        if (passiveItemSlots[i]!= null && passiveItemSlots[i].passiveItemData == chosenPassiveItemUpgrade.PassiveItemData)
                        {
                            newPassiveItem = false;

                            if (!newPassiveItem)
                            {
                                if (!chosenPassiveItemUpgrade.PassiveItemData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }


                                upgradeOption.upgradeButton.onClick.AddListener(()=>LevelUpPassiveItem(i,chosenPassiveItemUpgrade.passiveItemUpgradeIndex));

                                upgradeOption.UpgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.NextLevelPrefab.GetComponent<passiveItem>().passiveItemData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.NextLevelPrefab.GetComponent<passiveItem>().passiveItemData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newPassiveItem = true;
                        }
                    }

                    if(newPassiveItem)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(()=> player.SpawnPassiveItems(chosenPassiveItemUpgrade.initialPassiveItem));

                        upgradeOption.UpgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.PassiveItemData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenPassiveItemUpgrade.PassiveItemData.Icon;
                }            }
        }
    }


    void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption);
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }




    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }

    void EnableUpgradeUI(UpgradeUI ui)
    {
            ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
     }
}
