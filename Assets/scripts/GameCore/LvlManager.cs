using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlManager : MonoBehaviour
{
    [SerializeField] private Image LifeBar;
    [SerializeField] private Image ManaBar;

    [SerializeField] private TextMeshProUGUI PotionCount;
    
    [SerializeField] private Transform[] SpawnPoints;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform PJ = GameObject.FindWithTag("Player").transform;
        
        PJ.position = SpawnPoints[GameManager.instance.doorToGo].position;
        PJ.rotation = SpawnPoints[GameManager.instance.doorToGo].rotation;
        
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
    
}
