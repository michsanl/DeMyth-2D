using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using CustomTools.Core;
using MoreMountains.Tools;

public class SriAbilityNailAOE : Ability
{
    
    [Title("Components")]
    [SerializeField] private AnimationPropertiesSO _nailAOEProp;
    [SerializeField] private SriClipSO _sriClipSO;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject nailAOECollider;
    
    protected int NAIL_AOE = Animator.StringToHash("Nail_AOE");

    public override IEnumerator PlayAbility()
    {
        _animator.SetFloat("Nail_AOE_Multiplier", _nailAOEProp.AnimationSpeedMultiplier);
        
        _animator.SetTrigger(NAIL_AOE);
        Helper.PlaySFX(_sriClipSO.NailAOE, _sriClipSO.NailAOEVolume);

        yield return Helper.GetWaitForSeconds(_nailAOEProp.GetFrontSwingDuration());
        nailAOECollider.SetActive(true);
        yield return Helper.GetWaitForSeconds(_nailAOEProp.GetSwingDuration());
        nailAOECollider.SetActive(false);
        yield return Helper.GetWaitForSeconds(_nailAOEProp.GetBackSwingDuration());
    }
}