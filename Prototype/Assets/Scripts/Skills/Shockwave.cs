using UnityEngine;
using System.Collections;


public class Shockwave : SkillBase {
    public GameObject pre_Shockwave;
    public Transform playerTransform;
    private float speed = 15f;
    private float duration = 0.5f;

    private void Start() {
        cooldown = 1.0f;
    }



    protected override void Execute() {
        GameObject shockwave = Instantiate(pre_Shockwave, playerTransform.position, Quaternion.identity);
        StartCoroutine(MoveAndDestroy(shockwave));
    }


    private IEnumerator MoveAndDestroy(GameObject obj) {
        float elapsed = 0f;
        Vector3 dir = playerTransform.right; // �÷��̾��� �ٶ󺸴� ����, ���� �ʿ�

        while (elapsed < duration) {
            obj.transform.position += dir * speed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }

}