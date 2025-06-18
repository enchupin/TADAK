using UnityEngine;
using System.Collections;

public class PlayerSkillControl : MonoBehaviour
{
    public Shockwave ShockwaveSkill;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            ShockwaveSkill.UseSkill();
        }
    }
}


