using UnityEngine;
using System.Collections;


public abstract class SkillBase : MonoBehaviour {
    protected float cooldown;
    protected bool isReady = true;

    public void UseSkill() {
        if (isReady) {
            StartCoroutine(CastSkill());
        }
    }

    private IEnumerator CastSkill() {
        isReady = false;
        Execute();
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }

    protected abstract void Execute();

}