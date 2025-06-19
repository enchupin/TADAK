using UnityEngine;

public class MovingPlatformControl : MonoBehaviour
{
    public Transform b1;
    public Transform b2;
    public Transform b3;
    public Transform b4;
    public Transform b5;
    public Transform b6;
    public Transform b7;
    public Transform b8;
    private int mode = 0;

    public float lerpValue = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lerpValue > 1) {
            lerpValue = 0;
            mode++;
        }
        if (mode == 4)
            mode = 0;
        lerpValue += Time.deltaTime;
        if (mode == 0) {
            this.transform.position = BezierCurve(b1.position, b2.position, b3.position, b4.position, lerpValue);
        } else if (mode == 1) {
            this.transform.position = Vector3.Lerp(b4.position, b5.position, lerpValue);
        } else if (mode == 2) {
            this.transform.position = BezierCurve(b5.position, b6.position, b7.position, b8.position, lerpValue);
        } else if (mode == 3) {
            this.transform.position = Vector3.Lerp(b8.position, b1.position, lerpValue);
        }
    }

    public static Vector3 BezierCurve(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float value) {
        Vector3 a = Vector3.Lerp(p1, p2, value);
        Vector3 b = Vector3.Lerp(p2, p3, value);
        Vector3 c = Vector3.Lerp(p3, p4, value);

        Vector3 d = Vector3.Lerp(a, b, value);
        Vector3 e = Vector3.Lerp(b, c, value);

        Vector3 f = Vector3.Lerp(d, e, value);
        return f;
    }

}
