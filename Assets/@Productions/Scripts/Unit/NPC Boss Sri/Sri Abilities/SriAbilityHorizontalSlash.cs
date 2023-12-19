using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using CustomTools.Core;
using MoreMountains.Tools;

public class SriAbilityHorizontalSlash : MonoBehaviour
{
    [Title("Parameter Settings")]
    [SerializeField] private float frontSwingDuration;
    [SerializeField] private float swingDuration;
    [SerializeField] private float backSwingDuration;
    [SerializeField] private AnimationCurve animationCurve;
    
    [Title("Components")]
    [SerializeField] private GameObject horizontalSlashCollider;
    [SerializeField] private AbilityTimelineSO _abilityTimeline;
    [SerializeField] private Animator _animator;
    
    private int rightArenaBorder = 6;
    private int leftArenaBorder = -6;
    protected int HORIZONTAL_SLASH = Animator.StringToHash("Horizontal_Slash");

    private void Awake()
    {
        _animator.SetFloat("Hor_Slash_A", _abilityTimeline.AnticipationMultiplier);
        _animator.SetFloat("Hor_Slash_B", _abilityTimeline.AttackMultiplier);
        _animator.SetFloat("Hor_Slash_C", _abilityTimeline.RecoveryMultiplier);
    }

    public IEnumerator HorizontalSlash(Player player, Animator animator, AudioClip abilitySFX)
    {
        float playerXPosition = player.transform.position.x;
        float targetPosition = ClampValueToBattleArenaBorder(GetPositionWithIncrement(playerXPosition));
        int finalTargetPosition = Mathf.RoundToInt(targetPosition);

        animator.SetTrigger("Hor_Slash");
        PlayAudio(abilitySFX);

        yield return Helper.GetWaitForSeconds(_abilityTimeline.FinalAnticiptionDuration);
        horizontalSlashCollider.SetActive(true);
        yield return transform.DOMoveX(finalTargetPosition, _abilityTimeline.FinalAttackDuration).SetEase(animationCurve).WaitForCompletion();
        horizontalSlashCollider.SetActive(false);
        yield return Helper.GetWaitForSeconds(_abilityTimeline.FinalRecoveryDuration);
        
        // horizontalSlashCollider.SetActive(true);
        // yield return transform.DOMoveX(finalTargetPosition, swingDuration).SetEase(animationCurve).WaitForCompletion();
        // horizontalSlashCollider.SetActive(false);
        // yield return Helper.GetWaitForSeconds(backSwingDuration);
    }

    private void PlayAudio(AudioClip abilitySFX)
    {
        MMSoundManagerPlayOptions playOptions = MMSoundManagerPlayOptions.Default;
        playOptions.Volume = 1f;
        playOptions.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;

        MMSoundManagerSoundPlayEvent.Trigger(abilitySFX, playOptions);
    }

    private float GetPositionWithIncrement(float playerXPosition)
    {
        return playerXPosition > transform.position.x ? playerXPosition + 2 : playerXPosition -2;
    }

    private float ClampValueToBattleArenaBorder(float value)
    {
        return Mathf.Clamp(value, leftArenaBorder, rightArenaBorder);
    }
}
