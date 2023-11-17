using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public static CharacterSelector instance;

    public CharacterScripatableObject characterData;



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("EXTRA " + this + " Deleted");
            Destroy(gameObject);
        }
    }
  public static CharacterScripatableObject GetData()
    {
        return instance.characterData;
    }


    public void SelectCharacter(CharacterScripatableObject character)
    {
        characterData = character; 
    }


    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
    
}
