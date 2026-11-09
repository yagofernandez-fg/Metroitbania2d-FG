using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private Image LifeBar;
    [SerializeField] private Image ManaBar;

    [SerializeField] private TextMeshProUGUI PotionCount;
    
    [SerializeField] private Transform[] SpawnPoints;
    
    [SerializeField] private GameObject MessagePanel,gameOverPaner;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform PJ = GameObject.FindWithTag("Player").transform;


        if (GameManager.instance.IsFromLoad)
        {
            PJ.position = GameManager.instance.gameData.SpawnPoint;
            GameManager.instance.IsFromLoad = false;
        }
        else
        {
            PJ.position = SpawnPoints[GameManager.instance.doorToGo].position;
            PJ.rotation = SpawnPoints[GameManager.instance.doorToGo].rotation;
        }
        
        
        
        UpdateLifeBar();
        UpdateManaBar();
        UpdatePotionCount();
    }

    public void UpdateLifeBar()
    {
        LifeBar.fillAmount = GameManager.instance.gameData.Life/GameManager.instance.gameData.MaxLife;
    }

    public void UpdateManaBar()
    {
        ManaBar.fillAmount = GameManager.instance.gameData.Mana / GameManager.instance.gameData.MaxMana;
    }

    public void UpdatePotionCount()
    {
        PotionCount.text = "X"+GameManager.instance.gameData.PotionCount.ToString();
    }


    public void SowMensage(String Message)
    {
        MessagePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Message;
        MessagePanel.transform.GetChild(1).GetComponent<Button>().Select();
        MessagePanel.SetActive(true);
    }

    public void MessageButton()
    {
        Time.timeScale = 1;
        MessagePanel.SetActive(false);
    }
    
}
