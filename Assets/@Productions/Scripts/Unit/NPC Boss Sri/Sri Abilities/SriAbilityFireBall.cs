using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using CustomTools.Core;
using MoreMountains.Tools;

public class SriAbilityFireBall : MonoBehaviour
{
    [Title("Parameter Settings")]
    // [SerializeField] private float frontSwingDuration;
    // [SerializeField] private float swingDuration;
    // [SerializeField] private float backSwingDuration;
    [SerializeField] private float animationDuration;
    
    [Title("Components")]
    [SerializeField] private GameObject fireBallProjectile;
    [SerializeField] private Transform fireBallSpawnPosition;
    [SerializeField] private Animator _animator;
    
    protected int FIRE_BALL = Animator.StringToHash("Fire_Ball");

    public IEnumerator FireBall(Animator animator, AudioClip abilitySFX)
    {
        animator.Play(FIRE_BALL);
        PlayAudio(abilitySFX);

        Instantiate(fireBallProjectile, fireBallSpawnPosition.position, Quaternion.identity);
        yield return Helper.GetWaitForSeconds(animationDuration);
    }

    private void PlayAudio(AudioClip abilitySFX)
    {
        MMSoundManagerPlayOptions playOptions = MMSoundManagerPlayOptions.Default;
        playOptions.Volume = 1f;
        playOptions.MmSoundManagerTrack = MMSoundManager.MMSoundManagerTracks.Sfx;

        MMSoundManagerSoundPlayEvent.Trigger(abilitySFX, playOptions);
    }
}
