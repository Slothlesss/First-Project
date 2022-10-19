using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkillUI : MonoBehaviour
{
    public float rotateSpeed = 0.5f;
    protected bool canSwitchSkill = true;

    // Quay SkillUI len
    public void SwitchUp()
    {
        if (canSwitchSkill)
        {
            canSwitchSkill = false;
            transform.DORotate(new Vector3(0, 0, this.transform.eulerAngles.z - 180), rotateSpeed, RotateMode.FastBeyond360).OnComplete(ResetCanSwitchSkill);
        }
    }
    // Quay SkillUI xuong
    public void SwitchDown()
    {
        if (canSwitchSkill)
        {
            canSwitchSkill = false;
            transform.DORotate(new Vector3(0, 0, this.transform.eulerAngles.z + 180), rotateSpeed, RotateMode.FastBeyond360).OnComplete(ResetCanSwitchSkill);
        }
    }
    // Reset (bool)canSwitchSkill = true
    void ResetCanSwitchSkill() => this.canSwitchSkill = true;
}
