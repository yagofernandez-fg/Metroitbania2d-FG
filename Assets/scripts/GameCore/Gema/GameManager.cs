using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData gameData;
    public int doorToGo;
    public bool Intransition = false;
    public String SaveSlot;
    
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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        // JSON
        
        string json = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString(SaveSlot, json);
        
    }

    public void LoadData()
    {
        string Data = PlayerPrefs.GetString(SaveSlot);
        gameData = JsonUtility.FromJson<GameData>(Data);
        
    }
    
    
    
}
