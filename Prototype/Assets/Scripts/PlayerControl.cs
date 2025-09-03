using System.Collections;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour {
    private Rigidbody2D rb;
    private bool isGrounded = false;

    private float speed; // 최종 이동속도
    private float maxWalkSpeed = 5.0f; // 이동속도 계산 (속도 바꾸려면 이거 초기화 값 수정)
    private float jumpForce = 6.0f; // 점프력
    private float horizontalKey;
    public int gravityMode = 0; // 중력의 작용 방향 0 = 아래, 1 = 위, 2 = 좌측, 3 = 우측
    public float maxSlideSpeed = 100f;
    public float maxSlopeAngle = 30f;
    public float groundFriction = 15f; // 지면 마찰력

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

    private Vector2 slopeNormal = Vector2.up;

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            slopeNormal = contact.normal;
            // Debug.Log("바닥의 수직 벡터 : " + slopeNormal.x + " , " + slopeNormal.y);
            break;
        }
    }

    void PlayerMove()
    {
        horizontalKey = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalKey) > 0.01f)
        {
            // 직접 조작
            speed = horizontalKey * maxWalkSpeed;

            Vector3 scale = transform.localScale;
            scale.x = horizontalKey > 0 ? 1 : -1;
            transform.localScale = scale;

            this.rb.linearVelocity = new Vector2(speed, this.rb.linearVelocity.y);
        }
        else
        {
            if (isGrounded)
            {
                float slopeAngle = Vector2.Angle(slopeNormal, Vector2.up);

                if (slopeNormal != Vector2.up)
                {
                    Vector2 slideDirection = new Vector2(slopeNormal.y, -slopeNormal.x).normalized;

                    float slideAmount = Vector2.Dot(Physics2D.gravity.normalized, slideDirection);

                    speed = slideAmount * 5f;  // 슬라이드 속도

                    float finalSlideSpeed = speed;

                    rb.linearVelocity = slideDirection * finalSlideSpeed;
                }
                else if (slopeNormal == Vector2.up)
                {
                    Vector2 frictionVelocity = rb.linearVelocity;
                    frictionVelocity.x = Mathf.MoveTowards(rb.linearVelocity.x, 0f, groundFriction * Time.deltaTime);

                    rb.linearVelocity = frictionVelocity;
                }
                //if (slopeAngle > maxSlopeAngle) // 미끄러져야 할 경사면이라면
                //{
                //    // 미끄러짐 로직 (이전과 동일)
                //    Vector2 slideDirection = new Vector2(slopeNormal.y, -slopeNormal.x).normalized;
                //    float slideAmount = Vector2.Dot(Physics2D.gravity.normalized, slideDirection);
                //    float finalSlideSpeed = maxSlideSpeed * slideAmount;
                //    rb.linearVelocity = slideDirection * finalSlideSpeed;
                //}
                //else // 서 있을 수 있는 평지 또는 완만한 경사면이라면
                //{
                //    // 멈춤 로직 (이전과 동일)
                //    Vector2 frictionVelocity = rb.linearVelocity;
                //    frictionVelocity.x = Mathf.MoveTowards(rb.linearVelocity.x, 0f, groundFriction * Time.deltaTime);
                //    rb.linearVelocity = frictionVelocity;
                //}
            }
            else
            {
                this.rb.linearVelocity = new Vector2(0, this.rb.linearVelocity.y);
                speed = 0;
            }
            
        }
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
