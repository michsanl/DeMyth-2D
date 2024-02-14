﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Demyth.Gameplay;
using Core;
using MoreMountains.Tools;
using CustomExtensions;
using PixelCrushers;

public class TuyulFleeMovement : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 0.195f;
    [SerializeField] private Animator _animator;
    [SerializeField] private LayerMask _yulaPathLayerMask;
    [SerializeField] private LayerMask _moveBlockerLayerMask;

    private LookOrientation _lookOrientation;
    private bool _isBusy;
    private bool _isShocked;
    private bool _isMocking;
    private float _mockTimerMax = 2f;
    private float _mockTimer;
    private Coroutine _mockCoroutine;

    private PlayerManager _playerManager;

    private void Awake() 
    {
        _lookOrientation = GetComponent<LookOrientation>();
        _playerManager = SceneServiceProvider.GetService<PlayerManager>();
    }

    private void Start()
    {
        _mockTimer = _mockTimerMax;
    }
    
    private void Update()
    {
        MockingLoop();

        if (_isMocking)
        {
            _lookOrientation.SetFacingDirection(GetDirToPlayer());
        }
    }

    private void OnEnable() 
    {
        ResetUnitCondition();
    }

    public void StartFlee(Vector3 directionToPlayer)
    {
        if (_isBusy)
            return;

        TryFlee(directionToPlayer);
    }

    public void ResetUnitCondition()
    {
        _isShocked = false;
        _lookOrientation.SetFacingDirection(GetDirToPlayer());
        _animator.SetTrigger("Idle");
    }

    private void MockingLoop()
    {
        if (_isShocked)
            return;

        _mockTimer -= Time.deltaTime;
        if (_mockTimer <= 0)
        {
            if (_isBusy)
                return;

            _mockCoroutine = StartCoroutine(StartMocking());
            _mockTimer = _mockTimerMax;
        }
    }

    public void TryFlee(Vector3 directionToPlayer)
    {
        List<Vector3> nonFacingPlayerDirectionList = GetNonFacingPlayerDirections(directionToPlayer);

        foreach (var direction in nonFacingPlayerDirectionList)
        {
            if (IsFleePathAvailable(direction) && !IsPathBlocked(direction))
            {
                // can flee, move
                StopMocking();
                _isShocked = false;
                StartCoroutine(Move(direction));
                return;
            }
        }

        foreach (var direction in nonFacingPlayerDirectionList)
        {
            if (!IsPathBlocked(direction))
            {
                // can flee, move
                StopMocking();
                _isShocked = false;
                StartCoroutine(Move(direction));
                return;
            }
        }
        
        // cant flee, panik!
        StopMocking();
        _isShocked = true;
        _lookOrientation.SetFacingDirection(directionToPlayer);
        _animator.SetTrigger("Shock");
    }

    private IEnumerator Move(Vector3 moveDir)
    {
        _isBusy = true;

        _animator.Play("Dash");
        _lookOrientation.SetFacingDirection(moveDir);
        
        transform.DOMove(GetMoveTargetPositionRounded(moveDir), _moveDuration);
        yield return Helper.GetWaitForSeconds(_moveDuration);
        _mockTimer = 1f;

        _isBusy = false;
    }

    private IEnumerator StartMocking()
    {
        _isMocking = true;

        _animator.SetTrigger("Mock");
        yield return Helper.GetWaitForSeconds(1.875f);

        _isMocking = false;
    }

    private void StopMocking()
    {
        if (_mockCoroutine != null) StopCoroutine(_mockCoroutine);
        _isMocking = false;
    }

    private List<Vector3> GetNonFacingPlayerDirections(Vector3 directionToPlayer)
    {
        List<Vector3> nonFacingPlayerDirectionList = new();

        nonFacingPlayerDirectionList.Add(Vector2.Perpendicular(directionToPlayer));
        nonFacingPlayerDirectionList.Add(Vector2.Perpendicular(nonFacingPlayerDirectionList[0]));
        nonFacingPlayerDirectionList.Add(Vector2.Perpendicular(nonFacingPlayerDirectionList[1]));
        
        nonFacingPlayerDirectionList.Shuffle();
        return nonFacingPlayerDirectionList;
    }

    private bool IsFleePathAvailable(Vector3 dirToCheck)
    {
        return Physics2D.Raycast(transform.position + dirToCheck, dirToCheck, .1f, _yulaPathLayerMask);
    }

    private bool IsPathBlocked(Vector3 dirToCheck)
    {
        return Physics2D.Raycast(transform.position + dirToCheck, dirToCheck, .1f, _moveBlockerLayerMask);
    }

    private Vector3 GetMoveTargetPositionRounded(Vector3 moveDir)
    {
        var moveTargetPosition = transform.position + moveDir;
        moveTargetPosition.x = Mathf.RoundToInt(moveTargetPosition.x);
        moveTargetPosition.y = Mathf.RoundToInt(moveTargetPosition.y);
        return moveTargetPosition;
    }

    private Vector2 GetDirToPlayer()
    {
        if (_playerManager.Player.transform.position.x >= transform.position.x)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }
    }

}