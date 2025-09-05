using UnityEngine;
using System.Collections;

public class WindZone : MonoBehaviour
{
    private SpriteRenderer sr;
    private BoxCollider2D box;
    private PlayerControl pc;
    private Vector2 rightEdge;
    private Vector2 leftEdge;
    private float boxLength;
    private bool windState = false;
    private bool windCoroutineRunning = false;
    private bool pushState = false;

    // private float maxWindForce = 30f;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();

        Vector2 center = (Vector2)transform.position + box.offset;
        rightEdge = center + Vector2.right * box.size.x * 0.5f * transform.localScale.x;
        leftEdge = center - Vector2.right * box.size.x * 0.5f * transform.localScale.x;
        // boxLength = (rightEdge - leftEdge).magnitude;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (!windState) return;
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        pc = other.GetComponent<PlayerControl>();
        if (pc == null) return;

        if (!pushState) {
            StartCoroutine(PlayerInWind(pc));
        }
    }

    private void Update() {
        if (!(windState || windCoroutineRunning)) {
            StartCoroutine(WindStart());
        }
    }

    IEnumerator WindStart() {
        windCoroutineRunning = true;
        windState = true;
        sr.color = new Color32(255, 167, 165, 57);
        yield return new WaitForSeconds(0.5f);

        windState = false;
        sr.color = new Color32(165, 255, 242, 57);
        yield return new WaitForSeconds(3f);
        
        windCoroutineRunning = false;
    }

    /*
    IEnumerator PlayerInWind(Rigidbody2D rb, PlayerControl pc) {
        pushState = true;
        pc.canMoveState = false;
        //rb.AddForce(Vector2.left * windForce, ForceMode2D.Impulse);


        Vector2 playerPos = rb.transform.position;
        float posRatio = Mathf.Clamp01((playerPos.x - leftEdge.x) / boxLength);
        float windForce = Mathf.Lerp(0f, maxWindForce, posRatio);
        rb.AddForce(Vector2.left * windForce);


        yield return new WaitForSeconds(1f); // 밀릴 때 1초 경직
        pc.canMoveState = true;
        pushState = false;
    }*/

    IEnumerator PlayerInWind(PlayerControl pc) {
        pushState = true;
        pc.canMoveState = false;

        float startX = pc.transform.position.x;
        float targetX = leftEdge.x;

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Sqrt(elapsed / duration);


            float newX = Mathf.Lerp(startX, targetX, t);
            float currentY = pc.transform.position.y;

            pc.transform.position = new Vector2(newX, currentY);

            yield return null;
        }

        pc.transform.position = new Vector2(targetX, pc.transform.position.y);

        pc.canMoveState = true;
        pushState = false;
    }



}