using System;
using TMPro;
using UnityEngine;

[Serializable]
public class GameData
{
    [Header("Vida")] 
    [SerializeField] private float life;
    [SerializeField]private float maxLife;
    private bool Isdeath = false;
    [SerializeField] private int potionCount;
    [Header("Mana")] 
    [SerializeField] private float mana;
    [SerializeField]private float maxMana;
    [Header("Movility")]
    [SerializeField]private float maxJump;
    [Header("Damage")]
    [SerializeField]private float dmg;
    [SerializeField]private float magicDmg;
    [Header("Currensy")]
    [SerializeField]private int currency;
    
    [Header("SaveData")]
    [SerializeField] private string Sceneloc; 
    [SerializeField] private Vector3 SpawnPoints;


    public GameData()
    {
        this.life = 10;
        this.maxLife = 10;
        Isdeath = false;
        this.potionCount = 0;
        this.mana = 5;
        this.maxMana = 5;
        this.maxJump = 1;
        this.dmg = 2;
        this.currency = 0;
        Sceneloc = "Zona1";
        SpawnPoints = new Vector3(-7.7f,-6.9f,0f);
    }


    public float Life
    {
        get { return life; }
        set { life = value; }
    }
    
    public float MaxLife
    {
        get { return maxLife; }
        set { maxLife = value; }
    }

    public bool IsDeath
    {
        get { return Isdeath; }
        set { Isdeath = value; }
    }

    public float Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public float MaxMana
    {
        get { return maxMana; }
        set { maxMana = value; }
    }
    public float MaxJump
    {
        get { return maxJump; }
        set { maxJump = value; }
    }

    public float Dmg
    {
        get { return dmg; }
        set { dmg = value; }
    }
    
    public int Currency
    {
        get { return currency; }
        set { currency = value; }
    }
    
    public int PotionCount
    {
        get { return potionCount; }
        set { potionCount = value; }
    }

    public string SceneLoc
    {
        get { return Sceneloc; }
        set { Sceneloc = value; }
    }
    public Vector3 SpawnPoint
    {
        get { return SpawnPoints; }
        set { SpawnPoints = value; }
    }

    public float MagicDmg
    {
        get { return magicDmg; }
        set { magicDmg = value; }
    }
    
    

}
