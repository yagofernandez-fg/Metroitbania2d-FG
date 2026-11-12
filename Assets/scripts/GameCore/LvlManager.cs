using System;
using System.Collections;
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

    [SerializeField] private GameObject BarLife,BarMana;

    private float ActLife, ActMana;
    private Vector3 TInitLife, TInitMana;


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

        ActLife= GameManager.instance.gameData.Life;
        ActMana= GameManager.instance.gameData.Mana;

        TInitLife = BarLife.transform.position;
        TInitMana = BarMana.transform.position;

        UpdateLifeBar();
        UpdateManaBar();
        UpdatePotionCount();
    }

    public void UpdateLifeBar()
    {
        //LifeBar.fillAmount = GameManager.instance.gameData.Life/GameManager.instance.gameData.MaxLife;
        StartCoroutine(SmothDrainBar(LifeBar,GameManager.instance.gameData.Life, ActLife,GameManager.instance.gameData.MaxLife));
        ActLife = GameManager.instance.gameData.Life;
        StartCoroutine(ShackeBar(BarLife, TInitLife));
    }

    public void UpdateManaBar()
    {
        //ManaBar.fillAmount = GameManager.instance.gameData.Mana / GameManager.instance.gameData.MaxMana;
        StartCoroutine(SmothDrainBar(ManaBar, GameManager.instance.gameData.Mana, ActMana, GameManager.instance.gameData.MaxMana));
        ActMana = GameManager.instance.gameData.Mana;
        //StartCoroutine(ShackeBar(BarMana));
    }

    public void ShackManabar()
    {
        StartCoroutine(ShackeBar(BarMana, TInitMana));
        StartCoroutine(RedFlash(BarMana));
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


    IEnumerator SmothDrainBar(Image bar, float targetAmount, float current, float maxAmount)
    {
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            float currentAmount = Mathf.Lerp(current, targetAmount, Time.deltaTime * 10);
            current = currentAmount;
            bar.fillAmount = currentAmount / maxAmount;
            elapsed += Time.deltaTime;
            yield return null;
        }
        bar.fillAmount = targetAmount / maxAmount;
    }

    IEnumerator ShackeBar(GameObject Bar,Vector3 Tinit)
    {
        float elapsed = 0f;
        while (elapsed < 0.1f)//iterar durante 0.1s
        {
            float Ry = UnityEngine.Random.Range(Bar.transform.position.y - 2, Bar.transform.position.y + 2);
            float Rx = UnityEngine.Random.Range(Bar.transform.position.x - 2, Bar.transform.position.x + 2);
            Bar.transform.position = new Vector3(Rx, Ry, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0f;
        while (elapsed < 0.1f)//iterar durante 0.1s
        {
            float ActPosx = Mathf.Lerp(Bar.transform.position.x, Tinit.x, Time.deltaTime * 10);
            float ActPosyt = Mathf.Lerp(Bar.transform.position.y, Tinit.y, Time.deltaTime * 10);
            Bar.transform.position = new Vector3(ActPosx, ActPosyt, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Bar.transform.position = Tinit;
    }

    IEnumerator RedFlash(GameObject Bar)
    {
        float elapsed = 0f;

        Image[] images = Bar.GetComponentsInChildren<Image>();

        while (elapsed < 0.2f)
        {
            foreach (Image img in images)
                img.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            foreach (Image img in images)
                img.color = Color.white;

            yield return new WaitForSeconds(0.1f);

            elapsed += 0.2f;
        }
    }





    //IEnumerator SmothDrainLifeBar()
    //{
    //    float elapsed = 0f;
    //    while (elapsed < 0.2f)//iterar durante 0.2s
    //    {
    //        float currentAmount = Mathf.Lerp(ActLife, GameManager.instance.gameData.Life, Time.deltaTime*10);
    //        ActLife = currentAmount;
    //        LifeBar.fillAmount = currentAmount/GameManager.instance.gameData.MaxLife;
    //        elapsed += Time.deltaTime;
    //        yield return null;

    //    }
    //    ActLife = GameManager.instance.gameData.Life;
    //    LifeBar.fillAmount = ActLife/GameManager.instance.gameData.MaxLife;
    //}






    //IEnumerator ShackeBarLife()
    //{
    //    float elapsed = 0f;
    //    Vector3 TInit = BarLife.transform.position;
    //    while (elapsed < 0.1f)//iterar durante 0.1s
    //    {
    //        float Ry = UnityEngine.Random.Range(BarLife.transform.position.y-2, BarLife.transform.position.y + 2);
    //        float Rx = UnityEngine.Random.Range(BarLife.transform.position.x-2, BarLife.transform.position.x + 2);
    //        BarLife.transform.position = new Vector3(Rx, Ry, 0);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    elapsed = 0f;
    //    while (elapsed < 0.1f)//iterar durante 0.1s
    //    {
    //        float ActPosx = Mathf.Lerp(BarLife.transform.position.x, TInit.x,Time.deltaTime*10);
    //        float ActPosyt = Mathf.Lerp(BarLife.transform.position.y, TInit.y,Time.deltaTime*10);
    //        BarLife.transform.position = new Vector3(ActPosx, ActPosyt, 0);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    BarLife.transform.position = TInit;
    //}

    //IEnumerator SmothDrainManaBar()
    //{
    //    float elapsed = 0f;
    //    while (elapsed < 0.2f)//iterar durante 0.2s
    //    {
    //        float currentAmount = Mathf.Lerp(ActMana, GameManager.instance.gameData.Mana, Time.deltaTime * 10);
    //        ActMana = currentAmount;
    //        ManaBar.fillAmount = currentAmount / GameManager.instance.gameData.MaxMana;
    //        elapsed += Time.deltaTime;
    //        yield return null;

    //    }
    //    ActMana = GameManager.instance.gameData.Mana;
    //    ManaBar.fillAmount = ActMana / GameManager.instance.gameData.MaxMana;
    //}




    //IEnumerator ShackeBarMana()
    //{
    //    float elapsed = 0f;
    //    Vector3 TInit = BarMana.transform.position;
    //    while (elapsed < 0.1f)//iterar durante 0.1s
    //    {
    //        float Ry = UnityEngine.Random.Range(BarMana.transform.position.y - 2, BarMana.transform.position.y + 2);
    //        float Rx = UnityEngine.Random.Range(BarMana.transform.position.x - 2, BarMana.transform.position.x + 2);
    //        BarMana.transform.position = new Vector3(Rx, Ry, 0);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    elapsed = 0f;
    //    while (elapsed < 0.1f)//iterar durante 0.1s
    //    {
    //        float ActPosx = Mathf.Lerp(BarMana.transform.position.x, TInit.x, Time.deltaTime * 10);
    //        float ActPosyt = Mathf.Lerp(BarMana.transform.position.y, TInit.y, Time.deltaTime * 10);
    //        BarMana.transform.position = new Vector3(ActPosx, ActPosyt, 0);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    BarMana.transform.position = TInit;
    //}

}
