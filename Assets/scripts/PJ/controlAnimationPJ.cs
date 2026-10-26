using System;
using UnityEngine;

public class controlAnimationPJ : MonoBehaviour
{
    private PJController pjController;


    private void Start()
    {
        pjController = transform.parent.GetComponent<PJController>();
    }


    public void FinishAttack1()
    {
        pjController.finishAttack1();
    }

    public void FinishAttack2()
    {
        pjController.finishAttack2();
    }

}
