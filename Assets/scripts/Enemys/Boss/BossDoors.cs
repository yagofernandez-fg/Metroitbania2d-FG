using System;
using UnityEngine;

public class BossDoors : Door
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        StartBoss.OnCombatStart += CloseDors;
        BossController.OnBossDefeated += DoorStates;
    }

    private void CloseDors()
    {
        DoorStates(false);
    }

    private void OnDisable()
    {
        StartBoss.OnCombatStart -= CloseDors;
        BossController.OnBossDefeated -= DoorStates;
    }
}
