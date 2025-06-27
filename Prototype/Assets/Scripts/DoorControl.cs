using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour
{
    private float switchingTime = 1f;
    private Vector3 initPosition;
    private float lerpValue = 0;
    private bool openingNow = false;
    private bool closingNow = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (openingNow || closingNow) {
            lerpValue += Time.deltaTime;
        }
        if (lerpValue >= 1) {
            lerpValue = 0;
        }

    }
    IEnumerator openTimed() {
        yield return new WaitForSeconds(switchingTime);
        openingNow = false;
    }
    IEnumerator closeTimed() {
        yield return new WaitForSeconds(switchingTime);
        closingNow = false;
    }
    public void Open() {
        openingNow = true;
        this.transform.position = Vector3.Lerp(initPosition, initPosition + new Vector3(0, -1, 0), lerpValue);
        StartCoroutine(openTimed());
    }
    public void Close() {
        openingNow = false;
        this.transform.position = Vector3.Lerp(initPosition + new Vector3(0, -1, 0), initPosition, lerpValue);
        StartCoroutine(closeTimed());
    }
}
