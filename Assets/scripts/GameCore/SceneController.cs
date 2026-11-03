using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextZone(string sceneName)
    {
        StartCoroutine(loadLvl(sceneName));
    }


    IEnumerator loadLvl(string sceneName)
    {
        GameManager.instance.Intransition = true;
        animator.SetTrigger("EndLvl");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(sceneName);
        animator.SetTrigger("StartLvl");
        GameManager.instance.Intransition = false;
    }

}
