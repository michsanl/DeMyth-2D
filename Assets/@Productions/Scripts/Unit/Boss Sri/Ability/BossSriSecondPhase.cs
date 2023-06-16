using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class BossSriSecondPhase : BossSriAbility
{
    public bool ActivateSecondPhase
    {
        get => activateSecondPhase;
        set 
        {
            activateSecondPhase = value;
            
            if (activateSecondPhase == true)
            {
                Debug.Log("Second phase is active");
            }
        }
    }
    

    private bool activateSecondPhase;
    private Action[] movementActionPoolArray;
    private List<IEnumerator> abilityPoolList;


    protected override void OnActivate()
    {
        base.OnActivate();

        movementActionPoolArray = new Action[] {HorizontalMovement, VerticalMovement};
        abilityPoolList = new List<IEnumerator>() {PlaySpinClaw(), PlayNailAOE()};
    }

    protected override void OnTick()
    {
        base.OnTick();

        HandleAction();
    }

    private void HandleAction()
    {
        if (!activateSecondPhase)
            return;
        if (isBusy)
            return;

        SetFacingDirection();

        PlayTeleportAtRandomChance();

        if (IsPlayerNearby())
        {
            int randomIndex = UnityEngine.Random.Range(0, 3);
            if (randomIndex == 0)
            {
                StartCoroutine(PlayNailAOEShootingNail());
            }
            else
            {
                StartCoroutine(PlayNailAOEShootingNail());
            }
            return;
        }

        if (IsPlayerAtSamePosY())
        {
            PlayHorizontalAbility();
            return;
        }
        if (IsPlayerAtSamePosX())
        {
            PlayVerticalAbility();
            return;
        }


        if (!IsPlayerNearby())
        {
            int randomIndex = UnityEngine.Random.Range(0, 3);
            if (randomIndex == 0)
            {
                StartCoroutine(PlayNailSummon(groundNailPenta));
            }
            else
            {
                StartCoroutine(PlayFireBall());
            }
            
        }

    }

    private void PlayTeleportAtRandomChance()
    {
        if (UnityEngine.Random.Range(0, 3) == 0)
        {
            PlayTeleport();
            SetFacingDirection();
        }
    }

    private int GetRandomIndexFromList(List<IEnumerator> abilityList)
    {
        return UnityEngine.Random.Range(0, abilityList.Count);
    }

    private void PlayVerticalAbility()
    {
        if (IsPlayerAbove())
        {
            StartCoroutine(PlayUpSlash(Context.Player.transform.position.y + 1f));
            return;
        } else
        {
            StartCoroutine(PlayDownSlash(Context.Player.transform.position.y - 1f));
            return;
        }
    }

    private void PlayHorizontalAbility()
    {
        if (IsPlayerToRight())
        {
            StartCoroutine(PlayRightSlash(Context.Player.transform.position.x + 1f));
            return;
        } else
        {
            StartCoroutine(PlayLeftSlash(Context.Player.transform.position.x - 1f));
            return;
        }
    }


    private void HandleMovement()
    {
        if (isMoving)
            return;

        int i = UnityEngine.Random.Range(0,2);
        movementActionPoolArray[i]?.Invoke();
    }

    private void HorizontalMovement()
    {
        //SetFacingDirection();

        if (IsPlayerToRight())
        {
            StartCoroutine(PlayMove(Vector2.right));
            return;
        }
        if (IsPlayerToLeft())
        {
            StartCoroutine(PlayMove(Vector2.left));
            return;
        }
    }

    private void VerticalMovement()
    {
        //SetFacingDirection();

        if (IsPlayerAbove())
        {
            StartCoroutine(PlayMove(Vector2.up));
            return;
        }
        if (IsPlayerBelow())
        {
            StartCoroutine(PlayMove(Vector2.down));
            return;
        }
    }

    private void SetFacingDirection()
    {
        if (IsPlayerToRight())
        {
            lookOrientation.SetFacingDirection(Vector2.right);
        }

        if (IsPlayerToLeft())
        {
            lookOrientation.SetFacingDirection(Vector2.left);
        }
    }
}
