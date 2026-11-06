using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SaveStoneController : MonoBehaviour
{
    [SerializeField] private GameObject UIFlecha;
    [SerializeField] private GameObject UISafeAction;
    private bool InRange = false;
    private bool IsSaveing = false;
    [SerializeField] private ParticleSystem Saveparticles;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField] private float animSpeed;
    [SerializeField] private Light2D light;


    void Update()
    {
        if (!InRange || IsSaveing) return;
        if (Input.GetAxis("Vertical")>=0.5f)// guardamos
        {
            GameManager.instance.gameData.SceneLoc = SceneManager.GetActiveScene().name;
            GameManager.instance.gameData.SpawnPoint = this.transform.position;
            
            GameManager.instance.SaveData();
            
            StartCoroutine(Saving());
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !IsSaveing)
        {
            UIFlecha.SetActive(true);
            InRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" )
        {
            UIFlecha.SetActive(false);
            InRange = false;
        }
    }

    IEnumerator Saving()
    {
        IEnumerator animAlpha = AlphaAnm();
        StartCoroutine(animAlpha);
        
        UIFlecha.SetActive(false);
        Saveparticles.Play();
        IsSaveing = true;
        UISafeAction.SetActive(true);
        yield return new WaitForSeconds(1.65f);
        UISafeAction.SetActive(false);
        IsSaveing = false;
        Saveparticles.Stop();
        if (InRange) UIFlecha.SetActive(true);
        StopAllCoroutines();
    }

    IEnumerator AlphaAnm()
    {
        float t = 0;
        
        while(t<1)
        {
            t+= Time.deltaTime * animSpeed; 
            
            float alpha = Mathf.Lerp(0, 1, t);
            light.intensity = alpha*100;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return null;
        }
        
        while(t>0)
        {
            t-= Time.deltaTime * animSpeed; 
            
            float alpha = Mathf.Lerp(0, 1, t);
            light.intensity = alpha*100;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
            yield return null;
        }
        
        StartCoroutine(AlphaAnm());
    }
    
}
