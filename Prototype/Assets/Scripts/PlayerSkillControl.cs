using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;

public class SkillInfo {
    public float cooldown;
    public float duration;
    public bool isReady = true;
}

public static class SkillDatabase {
    public static Dictionary<string, SkillInfo> skills = new Dictionary<string, SkillInfo>() {
        { "shockwave", new SkillInfo { cooldown = 1f, duration = 0.5f } },
        { "activateShield", new SkillInfo { cooldown = 1f, duration = 0.5f } },
    };
}

public class PlayerSkillControl : MonoBehaviour
{
    public GameObject pre_Shockwave;
    private float speed = 15f;

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
        if (SkillDatabase.skills["shockwave"].isReady) {
            StartCoroutine(CastSkill("shockwave"));
            GameObject shockwave = Instantiate(pre_Shockwave, this.transform.position, Quaternion.identity);
            StartCoroutine(ShockwaveOperation(shockwave));

        }
    }


    void ActivateShield() {
        if (SkillDatabase.skills["activateShield"].isReady) {
            StartCoroutine(CastSkill("activateShield"));



        }
    }

    IEnumerator CastSkill(string skillName) {
        var skill = SkillDatabase.skills[skillName];
        skill.isReady = false;
        yield return new WaitForSeconds(skill.cooldown);
        skill.isReady = true;
    }

    IEnumerator ShockwaveOperation(GameObject obj) {
        float elapsed = 0f;
        Vector3 dir = this.transform.right; // 플레이어가 바라보는 방향, 수정 필요

        while (elapsed < SkillDatabase.skills["shockwave"].duration) {
            obj.transform.position += dir * speed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(obj);
    }


}