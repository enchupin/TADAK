using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class LeverTrigger : MonoBehaviour
{
    private bool useLever = false;
    public GameObject[] targetObject;
    private float duration = 10f;
    private float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useLever) {
            targetObject[0].transform.Translate(Time.deltaTime * speed, 0, 0);

        }
    }

    IEnumerator Timed() {
        shockwaveIsReady = false;
        yield return new WaitForSeconds(shockwaveCooldown);
        shockwaveIsReady = true;
    }

}
