using UnityEngine;
using System.Collections;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using System;

public class LeverTrigger : MonoBehaviour
{
    private bool useLever = false;
    private bool playerInRange = false;
    public GameObject[] targetObject;
    private float duration = 6f;
    private float cooldown = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !useLever && playerInRange) {
            useLever = true;
            StartCoroutine(Timed());
        }

    }

    IEnumerator Timed() {
        DoorOpen();
        yield return new WaitForSeconds(duration);
        DoorClose();
        yield return new WaitForSeconds(duration);
        useLever = false;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            playerInRange = false;
        }
    }


    private void DoorOpen() {
        foreach (GameObject door in targetObject) {
            if (door != null) {
                door.GetComponent<DoorControl>().Open();
            }
        }
    }

    private void DoorClose() {
        foreach (GameObject door in targetObject) {
            if (door != null) {
                door.GetComponent<DoorControl>().Close();
            }
        }
    }

}
