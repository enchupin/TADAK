using UnityEngine;

public class WindZone : MonoBehaviour
{
    private BoxCollider2D box;
    private Vector2 rightEdge;
    private Vector2 leftEdge;
    private float boxLength;

    private float maxWindForce = 400f;

    private void Awake() {
        box = GetComponent<BoxCollider2D>();
        Vector2 center = (Vector2)transform.position + box.offset;
        rightEdge = center + Vector2.right * box.size.x * 0.5f * transform.localScale.x;
        leftEdge = center - Vector2.right * box.size.x * 0.5f * transform.localScale.x;
        boxLength = (rightEdge - leftEdge).magnitude;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        Vector2 playerPos = other.transform.position;
        float posRatio = Mathf.Clamp01((playerPos.x - leftEdge.x) / boxLength);
        float windForce = Mathf.Lerp(0f, maxWindForce, posRatio);
        rb.AddForce(Vector2.left * windForce, ForceMode2D.Force);
    }
}