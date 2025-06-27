using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    float zoomSpeed = 10f;
    float targetZoom; // ¡‹ ¿Œ, ¡‹ æ∆øÙ 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.player = GameObject.Find("Player");
        targetZoom = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        Zoom();
    }

    void FollowPlayer()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }

    void Zoom()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))  // '['≈∞
            targetZoom = Mathf.Max(1f, targetZoom - 1f);

        if (Input.GetKeyDown(KeyCode.RightBracket))  // ']'≈∞
            targetZoom = Mathf.Min(20f, targetZoom + 1f);

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);
    }
}
