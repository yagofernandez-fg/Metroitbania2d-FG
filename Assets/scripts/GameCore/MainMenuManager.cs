using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuInicio,menuSlot;
    
    [SerializeField] private GameObject[] ButtonsSlot;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuInicio.SetActive(true);
    }

    public void PlayButton()
    {
        menuInicio.SetActive(false);
        menuSlot.SetActive(true);
        
        menuSlot.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().Select();
        InitSlots();
    }

    public void BackInit()
    {
        menuInicio.SetActive(true);
        menuSlot.SetActive(false);
        
        menuInicio.transform.GetChild(1).GetComponent<Button>().Select();
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private void InitSlots()
    {
        for(int i = 0; i < ButtonsSlot.Length; i++)
        {
            int index = i;
            if (PlayerPrefs.HasKey("GameData" + i.ToString()))
            {
                ButtonsSlot[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Juego : "+(i+1).ToString();
            }
            else
            {
                ButtonsSlot[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "No hay partida Guardada";
            }
            ButtonsSlot[i].GetComponent<Button>().onClick.RemoveAllListeners();
            ButtonsSlot[i].GetComponent<Button>().onClick.AddListener(delegate { SlotButton(index); });
        }
    }


    public void SlotButton(int i)
    {
        
        GameManager.instance.SaveSlot = "GameData" + i.ToString();
        if (PlayerPrefs.HasKey("GameData" + i.ToString()))
        {
            GameManager.instance.LoadData();
            
            GameManager.instance.IsFromLoad = true;
        }
        else
        {
            GameData StandarSave = new GameData();
            GameManager.instance.gameData = StandarSave;
        }
        SceneController.instance.NextZone(GameManager.instance.gameData.SceneLoc);
        
    }
}
