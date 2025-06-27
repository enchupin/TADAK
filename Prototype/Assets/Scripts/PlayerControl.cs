using System.Net;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private Rigidbody2D rb;
    private bool isGrounded = true;

    private float speed; // ���� �̵��ӵ�
    float targetSpeed; // �̵��ӵ� ���
    float maxWalkSpeed = 5.0f; // �̵��ӵ� ��� (�ӵ� �ٲٷ��� �̰� �ʱ�ȭ �� ����)
    private float jumpForce = 5; // ������

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        PlayerMove();
        PlayerJump();
    }

    void PlayerMove()
    {
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        targetSpeed = key * maxWalkSpeed;
        speed = Mathf.Lerp(this.rb.linearVelocity.x, targetSpeed, Time.deltaTime * 15);
        this.rb.linearVelocity = new Vector2(speed, this.rb.linearVelocity.y);

        // �����̴� ���⿡ ���� �÷��̾� ��� ���� (�ּ� ����Ǹ� �ּ� Ǯ� �����ϱ�)
        //if (key != 0)
        //{
        //    transform.localScale = new Vector3(key, 1, 1);
        //}
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
