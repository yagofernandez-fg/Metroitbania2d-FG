using System;
using UnityEngine;
[Serializable]
public class ScenarioData 
{
    [SerializeField] private bool doorOpen;
    [SerializeField] private bool firstBossDefeated;

    public ScenarioData()
    {
        doorOpen = false;
        firstBossDefeated = false;
    }

    public bool DoorOpen
    {
        get { return doorOpen; }
        set { doorOpen = value; }
    }

    public bool FirstBossDefeated
    {
        get { return firstBossDefeated; }
        set { firstBossDefeated = value; }
    }
}
