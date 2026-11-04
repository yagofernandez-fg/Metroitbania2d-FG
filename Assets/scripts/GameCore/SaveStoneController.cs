using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveStoneController : MonoBehaviour
{
    [SerializeField] private GameObject UIFlecha;
    [SerializeField] private GameObject UISafeAction;
    private bool InRange = false;
    private bool IsSaveing = false;

    void Update()
    {
        if (!InRange) return;
        if (Input.GetAxis("Vertical")>=0.5f)// guardamos
        {
            GameManager.instance.gameData.SceneLoc = SceneManager.GetActiveScene().buildIndex;
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
        if (other.tag == "Player" && !IsSaveing)
        {
            UIFlecha.SetActive(false);
            InRange = false;
        }
    }

    IEnumerator Saving()
    {
        UIFlecha.SetActive(false);
        IsSaveing = true;
        UISafeAction.SetActive(true);
        yield return new WaitForSeconds(1.65f);
        UISafeAction.SetActive(false);
        IsSaveing = false;
        if (InRange) UIFlecha.SetActive(true);
    }
    
}
