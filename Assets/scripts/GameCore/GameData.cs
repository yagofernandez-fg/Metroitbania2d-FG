using System;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField]private float life,maxLife;

    [SerializeField]private float mana, maxMana;

    [SerializeField]private float maxJump;

    [SerializeField]private float dmg;

    [SerializeField]private int currency;


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
    

}
