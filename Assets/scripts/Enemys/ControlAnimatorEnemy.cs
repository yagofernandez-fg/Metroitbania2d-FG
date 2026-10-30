using UnityEngine;

public class ControlAnimatorEnemy : MonoBehaviour
{
    
    public void GoblinAttack()
    {
        transform.parent.GetComponent<GoblinController>().shootLanza();
    }
    
    // mas funciones para llamar a los ataques
    
}
