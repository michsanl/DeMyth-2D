using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using CustomTools.Core;
using System;

public class PetraAbilityJumpGroundSlam : SceneService
{
    [Title("Animation Timeline")]
    [SerializeField] private float frontSwingDuration;
    [SerializeField] private float swingDuration;
    [SerializeField] private float backSwingDuration;
    
    [Title("Jump Parameter")]
    [SerializeField] private float jumpPower;
    [SerializeField] private AnimationCurve jumpCurve;
    
    [Title("Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject jumpSlamCollider;
    [SerializeField] private GameObject groundSlamCoffin;    


    public IEnumerator JumpGroundSlam(Action OnStart)
    {
        OnStart?.Invoke();

        Vector3 jumpTargetPosition = new Vector3(0, -1, 0);
        
        yield return Helper.GetWaitForSeconds(frontSwingDuration);

        yield return transform.DOJump(jumpTargetPosition, jumpPower, 1, swingDuration).SetEase(jumpCurve).WaitForCompletion();

        jumpSlamCollider.SetActive(true);
        Instantiate(groundSlamCoffin, transform.position, Quaternion.identity);

        yield return Helper.GetWaitForSeconds(backSwingDuration);

        jumpSlamCollider.SetActive(false);
    }
}
