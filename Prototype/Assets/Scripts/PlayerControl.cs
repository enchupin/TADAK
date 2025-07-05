using System.Collections;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private float speed; // 최종 이동속도
    private float maxWalkSpeed = 5.0f; // 이동속도 계산 (속도 바꾸려면 이거 초기화 값 수정)
    private float jumpForce = 6.0f; // 점프력
    private float horizontalKey;
    public int gravityMode = 0; // 중력의 작용 방향 0 = 아래, 1 = 위, 2 = 좌측, 3 = 우측

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        PlayerMove();
        PlayerJump();
        GravityControl();
    }

    void PlayerMove()
    {
        horizontalKey = Input.GetAxis("Horizontal");
        
        speed = horizontalKey * maxWalkSpeed;
        this.rb.linearVelocity = new Vector2(speed, this.rb.linearVelocity.y);

        // 움직이는 방향에 따라 플레이어 모습 반전 (애셋 적용되면 주석 풀어서 적용하기)
        
        /*
        if (Mathf.Round(horizontalKey) != 0)
        {
            transform.localScale = new Vector3(Mathf.Ceil(horizontalKey), 1, 1);
        }
        */
    }

    void PlayerJump() {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded) {
            switch (gravityMode) {
                case 0:
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    break;
                case 1:
                    rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                    break;
                case 2:
                    rb.AddForce(Vector2.right * jumpForce, ForceMode2D.Impulse);
                    break;
                case 3:
                    rb.AddForce(Vector2.left * jumpForce, ForceMode2D.Impulse);
                    break;
            }
            isGrounded = false;
        }
    }

    void GravityControl() {
        switch (gravityMode) {
            case 0:
                Physics2D.gravity = new Vector2(0, -9.8f);
                break;
            case 1:
                Physics2D.gravity = new Vector2(0, 9.8f);
                break;
            case 2:
                Physics2D.gravity = new Vector2(-9.8f, 0);
                break;
            case 3:
                Physics2D.gravity = new Vector2(9.8f, 0);
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }


}
