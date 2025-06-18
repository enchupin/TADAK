using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private float horizontal;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private float speed = 5; // 이동속도
    private float jumpForce = 5; // 점프력

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        PlayerMove();
    }


    void PlayerMove() {
        if (rb != null) {
            horizontal = Input.GetAxis("Horizontal");
            transform.Translate(horizontal * speed * Time.deltaTime, 0, 0);
            PlayerJump();
        }
    }

    void PlayerJump() {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    


}
