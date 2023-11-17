using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    // defining the diffrent states of the game 
   public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUP
    }

    public GameState currentState;

    public GameState previousState;


    [Header("Screens")]
    public GameObject pauseScreen;

    public GameObject resultsScrren;

    public GameObject LevelUpScreen;



    [Header("Current stat Displays")]
    //current stat displays 
    public TextMeshProUGUI currentHealthDisplay;
    public TextMeshProUGUI currentRecoveryDisplay;
    public TextMeshProUGUI currentMoveSpeedDisplay;
    public TextMeshProUGUI currentMightDisplay;
    public TextMeshProUGUI currentProjectileSpeedDisplay;
    public TextMeshProUGUI currentMagnetDisplay;



    [Header("Results screen display")]

    public Image chosenCharacterImage;
    public Text chosenCharacterName;

    public Text levelReachedDisplay;
    public Text timeSurvivedDisplay;

    public List<Image> chosenWeaponUI = new List<Image>(6);
    public List<Image> chosenPassiveItemUI = new List<Image>(6);

    [Header("Stopwatch")]
    public float timeLimit;
    float stopWatchTime;
    public Text stopwatchDisplay;


    public bool isGameOver = false;

    public bool choosingUpgrade;


    public GameObject playerObject; 

    public void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Extra " + this + " Deleted");
            Destroy(gameObject);
        }
        DisableScreen();
    }

    public void Update()
    {


        switch(currentState)
        {
            case GameState.Gameplay:
                // code for the gameplay
                CheckForPauseAndResume();
                UpdateStopwatch();
                break; 
            case GameState.Paused:
                //code for when paused 
                CheckForPauseAndResume();
                break; 
            case GameState.GameOver:

                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("Game Is Over");
                    DisplayResults();
                }
                // code for gameover 
                break;
            case GameState.LevelUP:
                if (!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;
                    Debug.Log("upgrades");
                    LevelUpScreen.SetActive(true);
                }
                break; 

            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break; 
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }


    public void PauseGame()
    {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Game is Paused");
        }
        

    }


    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1F;
            pauseScreen.SetActive(false);
            Debug.Log("Game is Paused");
        }
    }

    // define  the check of paused and unpaused
    public void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
         
            }
            else
            {
                 PauseGame();
            }
        }
    }


    void DisableScreen()
    {
        pauseScreen.SetActive(false);

        resultsScrren.SetActive(false);

        LevelUpScreen.SetActive(false);
    }




    public void GameOver()
    {
        timeSurvivedDisplay.text = stopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    public void DisplayResults()
    {
        resultsScrren.SetActive(true); 
    }


    public void AssignChosenCharacterUI(CharacterScripatableObject chosenCharacerData)
    {
        chosenCharacterImage.sprite = chosenCharacerData.Icon;
        chosenCharacterName.text = chosenCharacerData.Name;
    }


    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }


    public void AssignedChosenWeaponsAndPassiveItemsUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if (chosenWeaponsData.Count != chosenWeaponUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemUI.Count)
        {
            Debug.Log("CHOSEN WEAPONS AND PASSIVE ITEMS DATA LISTS HAVE LENGTHS ");
        }

        for ( int i = 0; i< chosenWeaponUI.Count; i++ )
        {
            if (chosenWeaponsData[i].sprite)
            {
                chosenWeaponUI[i].enabled = true;
                chosenWeaponUI[i].sprite = chosenWeaponsData[i].sprite;

            }
            else
            {
                chosenWeaponUI[i].enabled = false;
            }
        }

        for (int i = 0; i < chosenPassiveItemUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].sprite)
            {
                chosenPassiveItemUI[i].enabled = true;
                chosenPassiveItemUI[i].sprite = chosenPassiveItemsData[i].sprite;

            }
            else
            {
                chosenPassiveItemUI[i].enabled = false;
            }
        }
    }


    void UpdateStopwatch()
    {
        stopWatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();

        if (stopWatchTime >= timeLimit)
        {
            playerObject.SendMessage("Kill");
        }
    }


    void UpdateStopwatchDisplay()
    {
        // calculate the number of minutes and seconds that have elapsed
        int minutes = Mathf.FloorToInt(stopWatchTime / 60);
        int seconds = Mathf.FloorToInt(stopWatchTime % 60);

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUP);

        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }


    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1;
        LevelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
