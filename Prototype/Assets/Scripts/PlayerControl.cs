using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private float horizontal;
    private Rigidbody2D rb;
    private bool jumpState = true;

    private float speed = 5; // 이동속도
    private float jumpForce = 5; // 점프력

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        player_move();
    }


    void player_move() {
        if (rb != null) {
            horizontal = Input.GetAxis("Horizontal");
            transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);
            player_jump();
        }
    }

    void player_jump() {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && jumpState) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpState = false;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            jumpState = true;
        }
    }

}
