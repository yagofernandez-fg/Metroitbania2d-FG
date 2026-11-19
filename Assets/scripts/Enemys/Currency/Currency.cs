using System.Collections;
using UnityEngine;

public class Currency : MonoBehaviour
{
    [SerializeField] private float transitionSpeed = .2f;
    [SerializeField] private int currencyValue = 1;
    private Vector2 PJPos;

    private bool IsOsciling = true;


    [SerializeField] private float min = 0.5f;
    [SerializeField] private float max = 2f;
    [SerializeField] private float velo = 2f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (!IsOsciling) return;

        if (transform.position.y >= posicionInicial.y + max) velo = -Mathf.Abs(velo);
        else if (transform.position.y <= posicionInicial.y - min) velo = Mathf.Abs(velo);

        transform.position += Vector3.up * velo * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsOsciling = false;
            GetComponent<Collider2D>().enabled = false;
            PJPos = collision.transform.position;
            StartCoroutine(GetCoin());
        }
    }

    private IEnumerator GetCoin()
    {
        Vector2 TargetPos = new Vector2(PJPos.x-6, PJPos.y+4);
        float elapse = 0;
        while (elapse < transitionSpeed)
        {
            elapse += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime*10);
            yield return null;
        }
        transform.position = TargetPos;
        collectCoin();
    }

    private void collectCoin()
    {
        GameManager.instance.gameData.Currency += currencyValue;
        GameObject.Find("LvlManager").GetComponent<LvlManager>().UpdateCurrencyCount();
        Destroy(gameObject);
    }



}
