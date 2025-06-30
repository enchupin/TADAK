using System.Collections;
using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private float speed; // ���� �̵��ӵ�
    private float maxWalkSpeed = 5.0f; // �̵��ӵ� ��� (�ӵ� �ٲٷ��� �̰� �ʱ�ȭ �� ����)
    private float jumpForce = 6.0f; // ������
    private float horizontalKey;
    public int gravityMode = 0; // �߷��� �ۿ� ���� 0 = �Ʒ�, 1 = ��, 2 = ����, 3 = ����

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

        // �����̴� ���⿡ ���� �÷��̾� ��� ���� (�ּ� ����Ǹ� �ּ� Ǯ� �����ϱ�)
        
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
