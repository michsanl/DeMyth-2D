using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using CustomTools.Core;
using MoreMountains.Tools;
using Lean.Pool;

public class PetraAbilityChargeAttack : MonoBehaviour
{
    [Title("Parameter Settings")]
    [SerializeField] private float _frontSwingDuration = 0.6617f;
    [SerializeField] private float _swingDuration = 0.485f;
    [SerializeField] private float _backSwingDuration = 0.397f;
    
    [Title("Components")]
    [SerializeField] private GameObject groundCoffinAOE;
    
    private int CHARGE_ATTACK = Animator.StringToHash("Charge_attack");
    
    public IEnumerator ChargeAttack(Animator animator, PetraClipSO petraClipSO)
    {
        animator.Play(CHARGE_ATTACK);
        Helper.PlaySFX(petraClipSO.ChargeSlam, petraClipSO.ChargeSlamVolume);

        yield return Helper.GetWaitForSeconds(_frontSwingDuration);
        LeanPool.Spawn(groundCoffinAOE, transform.position, Quaternion.identity);

        yield return Helper.GetWaitForSeconds(_swingDuration);

        yield return Helper.GetWaitForSeconds(_backSwingDuration);
    }

    private void PlayAudio(AudioClip abilitySFX)
    {
        MMSoundManagerPlayOptions playOptions = MMSoundManagerPlayOptions.Default;
        playOptions.Volume = 1f;
        playOptions.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;

        MMSoundManagerSoundPlayEvent.Trigger(abilitySFX, playOptions);
    }
}
