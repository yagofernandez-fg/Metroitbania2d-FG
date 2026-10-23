using Unity.VisualScripting;
using UnityEngine;

public class CaneraController : MonoBehaviour
{
    [SerializeField] private Transform PjTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField][Tooltip("X=min , Y=max")] private Vector2 limitX;
    [SerializeField][Tooltip("X=min , Y=max")] private Vector2 limitY;
    [SerializeField]private ParalaxEffect[]  paralaxEffects;

    void LateUpdate()
    {
        
        float x =Mathf.Clamp(PjTransform.position.x+offset.x, limitX.x, limitX.y);
        float y =Mathf.Clamp(PjTransform.position.y+offset.y, limitY.x, limitY.y);
        float z = PjTransform.position.z+offset.z;
        
        transform.position = new Vector3(x,y,z);
        // foreach (ParalaxEffect paralax in paralaxEffects) Menos efficiente
        // {
        //     paralax.UpdateParalaxEffect();
        // }

        for (int i = 0; i < paralaxEffects.Length; i++)
        {
            paralaxEffects[i].UpdateParalaxEffect();
        }
    }

}
