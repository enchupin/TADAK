using UnityEngine;

public class WindZone : MonoBehaviour
{
    public float windForce = 250f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Rigidbody2D rb = other.attachedRigidbody;
            if (rb != null) {
                rb.AddForce(Vector2.left * windForce);
            }
        }
    }
}