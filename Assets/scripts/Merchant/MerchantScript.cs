using System;
using TMPro;
using UnityEngine;

public class MerchantScript : MonoBehaviour
{
    [SerializeField] private GameObject merchantCamvas;
    [SerializeField] private int damegeCost, defenceCost;
    [SerializeField] private TextMeshProUGUI damageTextCost,defenceTextCost;
    private bool Islocked = true;

    private void Start()
    {
        damageTextCost.text = damegeCost.ToString();
        defenceTextCost.text = defenceCost.ToString();
    }


    public void buyDamage()
    {
        if (Islocked) return;
        if (GameManager.instance.gameData.Currency >= damegeCost)
        {
            GameManager.instance.gameData.Currency -= damegeCost;
            GameManager.instance.gameData.Dmg += 1;
        }
        else
        {
            // no tines dinero
        }
    }

    public void buyDefence()
    {
        if (Islocked) return;
        if (GameManager.instance.gameData.Currency >= damegeCost)
        {
            GameManager.instance.gameData.Currency -= defenceCost;
            GameManager.instance.gameData.MaxLife += 3;
            GameManager.instance.gameData.Life += 3;
        }
        else
        {
            // no tines dinero
        }
    }
    public void closeShop()
    {
        
    }
}
