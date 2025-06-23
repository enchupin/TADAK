using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class LeverTrigger : MonoBehaviour
{
    private bool useLever = false;
    public GameObject[] targetObject;
    private float duration = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    IEnumerator Timed() {
        DoorOpen();
        yield return new WaitForSeconds(duration);
        DoorClose();
        useLever = false;
    }

        private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Trigger" && Input.GetKeyDown(KeyCode.Space) && !useLever) {
            useLever = true;
            StartCoroutine(Timed());
        }
    }

    private void DoorOpen() {
        foreach (GameObject door in targetObject) {
            if (door != null) {
                // Assuming the doors have a script to handle opening
                door.GetComponent<Door>().Open();
            }
        }
    }

    private void DoorClose() {
        foreach (GameObject door in targetObject) {
            if (door != null) {
                // Assuming the doors have a script to handle closing
                door.GetComponent<Door>().Close();
            }
        }
    }

}
