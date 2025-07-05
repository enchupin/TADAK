using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour
{
    private float switchingTime = 5f; // 문 여는데 걸리는 시간
    private Vector3 initPosition;
    private Vector3 endPosition;
    private float lerpValue = 0;
    private bool openingNow = false;
    private bool closingNow = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initPosition = this.transform.position;
        endPosition = initPosition + new Vector3(0, -3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (openingNow) {
            lerpValue += Time.deltaTime / switchingTime;
            this.transform.position = Vector3.Lerp(initPosition, endPosition, lerpValue);

            if (lerpValue >= 1f) {
                transform.position = endPosition;
                openingNow = false;
                lerpValue = 0f;
            }
        }

        else if (closingNow) {
            lerpValue += Time.deltaTime / switchingTime;
            this.transform.position = Vector3.Lerp(endPosition, initPosition, lerpValue);

            if (lerpValue >= 1f) {
                transform.position = initPosition;
                closingNow = false;
                lerpValue = 0f;
            }
        }




    }




    public void Open() {
        if (!(openingNow || closingNow)) {
            openingNow = true;
            lerpValue = 0f;
        }
    }
    public void Close() {
        if (!(openingNow || closingNow)) {
            closingNow = true;
            lerpValue = 0f;
        }
    }
}
