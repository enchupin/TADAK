using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private float breakForce = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            float impactForce = collision.relativeVelocity.magnitude * rb.mass;

            if (impactForce >= breakForce)
            {
                Destroy(gameObject); // 애셋 생기면 적용 예정
            }
        }
    }
}
