using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerSkillControl : MonoBehaviour
{
    public GameObject pre_Shockwave;
    
    private float speed = 15f; // 쇼크웨이브 투사체 속도
    private float shockwaveDuration = 0.5f; // 쇼크웨이브 지속시간
    private float shockwaveCooldown = 1f; // 쇼크웨이브 쿨타임
    private bool shockwaveIsReady = true;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            Shockwave();
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            ActivateShield();
        }

    }




    void Shockwave() {
        if (shockwaveIsReady) {
            StartCoroutine(CastSkill());
            GameObject shockwave = Instantiate(pre_Shockwave, this.transform.position, Quaternion.identity);
            StartCoroutine(MoveAndDestroy(shockwave));

        }
    }
    
    IEnumerator CastSkill() {
        shockwaveIsReady = false;
        yield return new WaitForSeconds(shockwaveCooldown);
        shockwaveIsReady = true;
    }

    IEnumerator MoveAndDestroy(GameObject obj) {
        float elapsed = 0f;
        Vector3 dir = this.transform.right; // 플레이어의 바라보는 방향, 수정 필요

        while (elapsed < shockwaveDuration) {
            obj.transform.position += dir * speed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }

    void ActivateShield() {

    }


}


