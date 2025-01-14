using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using CustomTools.Core;
using MoreMountains.Tools;

public class SriAbilityDownSlash : MonoBehaviour
{
    [Title("Parameter Settings")]
    [SerializeField] private float frontSwingDuration;
    [SerializeField] private float swingDuration;
    [SerializeField] private float backSwingDuration;
    [SerializeField] private AnimationCurve animationCurve;
    
    [Title("Components")]
    [SerializeField] private AnimationPropertiesSO _downSlashProp;
    [SerializeField] private GameObject downSlashCollider;
    [SerializeField] private SriClipSO _sriClipSO;
    
    private int topArenaBorder = 2;
    private int bottomArenaBorder = -4;
    private int DOWN_SLASH = Animator.StringToHash("Down_Slash");

    public IEnumerator DownSlash(Player player, Animator animator)
    {
        animator.SetFloat("Ver_Slash_Multiplier", _downSlashProp.AnimationSpeedMultiplier);
        
        var playerYPosition = player.transform.position.y;
        var targetPosition = ClampValueToBattleArenaBorder(GetPositionWithIncrement(playerYPosition));
        int finalTargetPosition = Mathf.RoundToInt(targetPosition);

        animator.SetTrigger(DOWN_SLASH);
        Helper.PlaySFX(_sriClipSO.VerticalSlash, _sriClipSO.VerticalSlashVolume);

        yield return Helper.GetWaitForSeconds(_downSlashProp.GetFrontSwingDuration());
        downSlashCollider.SetActive(true);
        yield return transform.DOMoveY(finalTargetPosition, _downSlashProp.GetSwingDuration()).SetEase(animationCurve).WaitForCompletion();
        downSlashCollider.SetActive(false);
        yield return Helper.GetWaitForSeconds(_downSlashProp.GetBackSwingDuration());
    }

    private void PlayAudio(AudioClip abilitySFX)
    {
        MMSoundManagerPlayOptions playOptions = MMSoundManagerPlayOptions.Default;
        playOptions.Volume = 1f;
        playOptions.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;

        MMSoundManagerSoundPlayEvent.Trigger(abilitySFX, playOptions);
    }

    private float GetPositionWithIncrement(float playerYPosition)
    {
        return playerYPosition - 2;
    }

    private float ClampValueToBattleArenaBorder(float value)
    {
        return Mathf.Clamp(value, bottomArenaBorder, topArenaBorder);
    }
}
