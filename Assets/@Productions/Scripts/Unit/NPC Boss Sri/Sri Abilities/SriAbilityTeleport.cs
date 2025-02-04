using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools.Core;
using MoreMountains.Tools;

public class SriAbilityTeleport : MonoBehaviour
{
    [SerializeField] private AnimationPropertiesSO _teleportProp;
    [SerializeField] private float teleportStartDuration;
    [SerializeField] private float teleportEndDuration;
    [SerializeField] private Animator animator;
    [SerializeField] private SriClipSO _sriClipSO;
    
    private int topBorder = 2;
    private int bottomBorder = -4;
    private int rightBorder = 6;
    private int leftBorder = -6;
    private Vector3[] pillarPositionArray = new Vector3[] 
    { 
        new(5,1,0), new(-5,1,0), new(5,-1,0), new(-5,-1,0)
    };
    private int TELEPORT_START = Animator.StringToHash("Teleport_Start");
    private int TELEPORT_END = Animator.StringToHash("Teleport_End");

    public IEnumerator Teleport(Player player, Animator animator)
    {
        animator.SetFloat("Teleport_Multiplier", _teleportProp.AnimationSpeedMultiplier);
        
        animator.SetTrigger(TELEPORT_START);
        yield return Helper.GetWaitForSeconds(_teleportProp.GetFrontSwingDuration());

        var teleportTargetPosition = GetTeleportTargetPosition(player);
        transform.position = teleportTargetPosition;

        animator.SetTrigger(TELEPORT_END);
        yield return Helper.GetWaitForSeconds(_teleportProp.GetBackSwingDuration());
    }

    public IEnumerator Teleport(Vector3 targetPosition, Animator animator)
    {
        animator.SetFloat("Teleport_Multiplier", _teleportProp.AnimationSpeedMultiplier);

        animator.Play(TELEPORT_START);
        yield return Helper.GetWaitForSeconds(_teleportProp.GetFrontSwingDuration());

        transform.position = targetPosition;

        animator.Play(TELEPORT_END);
        yield return Helper.GetWaitForSeconds(_teleportProp.GetBackSwingDuration());
    }

    private Vector2 GetTeleportTargetPosition(Player player)
    {
        Vector3 targetPosition = player.LastMoveTargetPosition;
        int randomIndex = UnityEngine.Random.Range(0, 4);

        switch (randomIndex)
        {
            case 0:
                targetPosition.x = targetPosition.x + GetPositionOffset();
                break;
            case 1:
                targetPosition.x = targetPosition.x - GetPositionOffset();
                break;
            case 2:
                targetPosition.y = targetPosition.y + GetPositionOffset();
                break;
            case 3:
                targetPosition.y = targetPosition.y - GetPositionOffset();
                break;
        }

        if (IsTargetPositionSamePlace(targetPosition) || IsOutOfBounds(targetPosition))
        {
            return GetTeleportTargetPosition(player);
        }
        else
        {
            return targetPosition;
        }
    }

    private int GetPositionOffset()
    {
        return UnityEngine.Random.Range(2, 4);
    }

    private bool IsTargetPositionSamePlace(Vector3 targetPosition)
    {
        return targetPosition == transform.position;
    }

    private bool IsOutOfBounds(Vector3 targetPosition)
    {
        float positionY = targetPosition.y;
        float positionX = targetPosition.x;

        return positionY > topBorder || positionY < bottomBorder || positionX > rightBorder || positionX < leftBorder;
    }

    private bool IsTargetPillarPosition(Vector3 targetPosition)
    {
        foreach (var pillarPosition in pillarPositionArray)
        {
            if (targetPosition == pillarPosition)
                return true;
        }
        return false;
    }
}
