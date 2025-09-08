using System.Collections;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour {
    private Rigidbody2D rb;
    private bool isGrounded = false;

    private float speed; // ���� �̵��ӵ�
    private float maxWalkSpeed = 5.0f; // �̵��ӵ� ��� (�ӵ� �ٲٷ��� �̰� �ʱ�ȭ �� ����)
    private float jumpForce = 6.0f; // ������
    private float horizontalKey;
    public int gravityMode = 0; // �߷��� �ۿ� ���� 0 = �Ʒ�, 1 = ��, 2 = ����, 3 = ����
    public float maxSlideSpeed = 100f;
    public float maxSlopeAngle = 30f;
    public float groundFriction = 15f; // ���� ������

    public bool canMoveState = true;
    public bool isMoveState = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        if (canMoveState) {
            PlayerMove();
            PlayerJump();
        }
        GravityControl();
    }

    private Vector2 slopeNormal = Vector2.up;

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            slopeNormal = contact.normal;
            // Debug.Log("�ٴ��� ���� ���� : " + slopeNormal.x + " , " + slopeNormal.y);
            break;
        }
    }

    void PlayerMove()
    {
        horizontalKey = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontalKey) > 0.01f)
        {
            isMoveState = true;
            // ���� ����
            speed = horizontalKey * maxWalkSpeed;

            Vector3 scale = transform.localScale;
            scale.x = horizontalKey > 0 ? 1 : -1;
            transform.localScale = scale;

            this.rb.linearVelocity = new Vector2(speed, this.rb.linearVelocity.y);
        }
        else
        {
            isMoveState = false;
            if (isGrounded)
            {
                if (slopeNormal != Vector2.up)
                {
                    Vector2 slideDirection = new Vector2(slopeNormal.y, -slopeNormal.x).normalized;

                    float slideAmount = Vector2.Dot(Physics2D.gravity.normalized, slideDirection);

                    speed = slideAmount * 10f;  // �����̵� �ӵ�

                    float finalSlideSpeed = speed;

                    rb.linearVelocity = slideDirection * finalSlideSpeed;
                }
                else if (slopeNormal == Vector2.up)
                {
                    Vector2 frictionVelocity = rb.linearVelocity;
                    frictionVelocity.x = Mathf.MoveTowards(rb.linearVelocity.x, 0f, groundFriction * Time.deltaTime);

                    rb.linearVelocity = frictionVelocity;
                }
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
