using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaneraController : MonoBehaviour
{
    [SerializeField] private Transform PjTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField][Tooltip("X=min , Y=max")] private Vector2 limitX;
    [SerializeField][Tooltip("X=min , Y=max")] private Vector2 limitY;
    [SerializeField]private ParalaxEffect[]  paralaxEffects;
    
    private bool IsShacking = false;

    // private void Update()
    // {
    //     if (Input.GetKeyDown("l"))
    //     {
    //         print("Shake");
    //         StartCoroutine(ShackeCamera(2,20,.01f,1));
    //     }
    // }

    void LateUpdate()
    {
        if (!IsShacking)
        {
            float x =Mathf.Clamp(PjTransform.position.x+offset.x, limitX.x, limitX.y);
            float y =Mathf.Clamp(PjTransform.position.y+offset.y, limitY.x, limitY.y);
            float z = PjTransform.position.z+offset.z;
        
            transform.position = new Vector3(x,y,z);
        }

        // foreach (ParalaxEffect paralax in paralaxEffects) Menos efficiente
        // {
        //     paralax.UpdateParalaxEffect();
        // }

        for (int i = 0; i < paralaxEffects.Length; i++)
        {
            paralaxEffects[i].UpdateParalaxEffect();
        }
    }

    public void StartShacke(float time,float force,float frecuency,float smothness)
    {
        StartCoroutine(ShackeCamera(time,force,frecuency,smothness));
    }

    private IEnumerator ShackeCamera(float time,float force,float frecuency,float smothness)
    {
        IsShacking = true;
        float elapse = 0;
        float timeFrecuency = frecuency+1;
        Vector3 TargetPos = Vector3.zero,InitialPos;

        float xt=0, yt=0,zt=0;
        
        while (elapse<time)
        {
            if (timeFrecuency >= frecuency)
            {
                xt = PjTransform.position.x+Random.Range(-force, force);
                yt =  PjTransform.position.y+Random.Range(-force, force);
                zt = PjTransform.position.z+Random.Range(-force, force);
                
                timeFrecuency = 0;
            }
            
            
            float x =Mathf.Clamp(PjTransform.position.x+offset.x, limitX.x, limitX.y);
            float y =Mathf.Clamp(PjTransform.position.y+offset.y, limitY.x, limitY.y);
            float z = PjTransform.position.z+offset.z;
        
            InitialPos = new Vector3(x,y,z);
            
            TargetPos = new Vector3(x+xt, y+yt, z+zt);
            
            transform.position = Vector3.Lerp(InitialPos,TargetPos,smothness*Time.deltaTime);
            elapse += Time.deltaTime;
            timeFrecuency += Time.deltaTime;
            yield return null;
            
        }
        
        yield return null;
        IsShacking = false;
    }

}
