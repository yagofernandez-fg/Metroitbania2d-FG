using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData gameData;
    public int doorToGo;
    public bool Intransition = false;
    public String SaveSlot;
    public ScenarioData scenarioData;
    public bool IsFromLoad = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SaveData()
    {
        // JSON
        
        string jsonGD = JsonUtility.ToJson(gameData);
        string jsonSD = JsonUtility.ToJson(scenarioData);
        PlayerPrefs.SetString(SaveSlot, jsonGD);
        PlayerPrefs.SetString(SaveSlot+"Scenario", jsonSD);
        
    }

    public void LoadData()
    {
        string jsonGD = PlayerPrefs.GetString(SaveSlot);
        gameData = JsonUtility.FromJson<GameData>(jsonGD);
        
        string jsonSD = PlayerPrefs.GetString(SaveSlot+"Scenario");
        scenarioData = JsonUtility.FromJson<ScenarioData>(jsonSD);
        
        
        
    }
    
    
    
}
