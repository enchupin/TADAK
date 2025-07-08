using UnityEngine;
using System.Collections;

public class WindZone : MonoBehaviour
{
    private BoxCollider2D box;
    private Vector2 rightEdge;
    private Vector2 leftEdge;
    private float boxLength;
    private bool windCooldown = false;

    public float maxWindForce = 4000f;

    private void Awake() {
        box = GetComponent<BoxCollider2D>();
        Vector2 center = (Vector2)transform.position + box.offset;
        rightEdge = center + Vector2.right * box.size.x * 0.5f * transform.localScale.x;
        leftEdge = center - Vector2.right * box.size.x * 0.5f * transform.localScale.x;
        boxLength = (rightEdge - leftEdge).magnitude;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (windCooldown) return;
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        PlayerControl pc = other.GetComponent<PlayerControl>();
        if (pc == null) return;

        StartCoroutine(WindCooldown(rb, pc));
    }

    IEnumerator WindCooldown(Rigidbody2D rb, PlayerControl pc) {
        windCooldown = true;
        pc.canMoveState = false;

        rb.AddForce(Vector2.left * maxWindForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(5f);

        pc.canMoveState = true;
        windCooldown = false;
    }


}