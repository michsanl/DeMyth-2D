using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using CustomTools.Core;
using Core;
using Demyth.Gameplay;
using UnityEditor.ShortcutManagement;

public class PetraCombatBehaviorNew : MonoBehaviour
{
    [SerializeField] private bool _activateCombatBehaviorOnStart;
    [SerializeField] private int _phaseTwoHPTreshold;
    [SerializeField, EnumToggleButtons] private CombatMode _selectedCombatMode;
    [SerializeField, EnumToggleButtons] private Ability _loopAbility;
    [SerializeField] private GameObject[] _attackColliderArray;
    [SerializeField] private Animator _animator;
    
    private enum Ability 
    { UpCharge, DownCharge, HorizontalCharge, SpinAttack, ChargeAttack, BasicSlam, JumpSlam }
    private enum CombatMode 
    { FirstPhase, SecondPhase, AbilityLoop }

    private PetraAbilityUpCharge _upChargeAbility;
    private PetraAbilityDownCharge _downChargeAbility;
    private PetraAbilityHorizontalCharge _horizontalChargeAbility;
    private PetraAbilitySpinAttack _spinAttackAbility;
    private PetraAbilityChargeAttack _chargeAttackAbility;
    private PetraAbilityBasicSlam _basicSlamAbility;
    private PetraAbilityJumpSlam _jumpSlamAbility;
    private PetraAbilityJumpGroundSlam _jumpGroundSlamAbility;

    private CombatMode _currentCombatMode;
    private PlayerManager _playerManager;
    private Player _player;
    private LookOrientation _lookOrientation;
    private Health _health;
    private int _lastRandomResult;
    private int _consecutiveCount;
    private float _timeVarianceCompensationDelay = 0.05f;

    private void Awake()
    {
        _playerManager = SceneServiceProvider.GetService<PlayerManager>();
        _lookOrientation = GetComponent<LookOrientation>();
        _health = GetComponent<Health>();
        _player = _playerManager.Player;

        _upChargeAbility = GetComponent<PetraAbilityUpCharge>();
        _downChargeAbility = GetComponent<PetraAbilityDownCharge>();
        _horizontalChargeAbility = GetComponent<PetraAbilityHorizontalCharge>();
        _spinAttackAbility = GetComponent<PetraAbilitySpinAttack>();
        _chargeAttackAbility = GetComponent<PetraAbilityChargeAttack>();
        _basicSlamAbility = GetComponent<PetraAbilityBasicSlam>();
        _jumpSlamAbility = GetComponent<PetraAbilityJumpSlam>();
        _jumpGroundSlamAbility = GetComponent<PetraAbilityJumpGroundSlam>();
    }

    private void Start()
    {
        _health.OnTakeDamage += Health_OnTakeDamage;
        _health.OnDeath += Health_OnDeath;

        if (_activateCombatBehaviorOnStart)
            ChangeCombatBehavior();
    }

    private void Update()
    {
        if (!_activateCombatBehaviorOnStart)
            return;

        UpdateCombatMode();
    }

    private void UpdateCombatMode()
    {
        if (_currentCombatMode != _selectedCombatMode)
        {
            _currentCombatMode = _selectedCombatMode;
            ChangeCombatBehavior();
        }
    }

    private void Health_OnTakeDamage()
    {
        if (_health.CurrentHP == _phaseTwoHPTreshold)
        {
            StopCurrentAbility();
            StartCoroutine(StartPhaseTwo());
        }
    }

    private void Health_OnDeath()
    {
        StopCurrentAbility();
        _animator.Play("Defeated");
    }
    
    private void ChangeCombatBehavior()
    {
        StopCurrentAbility();

        switch (_selectedCombatMode)
        {
            case CombatMode.FirstPhase:
                StartCoroutine(LoopAbility(GetFirstPhaseAbility));
                break;
            case CombatMode.SecondPhase:
                StartCoroutine(LoopAbility(GetSecondPhaseAbility));
                break;
            case CombatMode.AbilityLoop:
                StartCoroutine(LoopAbility(GetAbilityTesterAbility));
                break;
        }
    }

    private IEnumerator LoopAbility(Func<IEnumerator> selectedPhaseAbility)
    {
        IEnumerator ability = selectedPhaseAbility();

        SetFacingDirection();
        yield return StartCoroutine(ability);
        yield return Helper.GetWaitForSeconds(_timeVarianceCompensationDelay);

        StartCoroutine(LoopAbility(selectedPhaseAbility));
    }
    
    private IEnumerator GetFirstPhaseAbility()
    {
        if (IsPlayerNearby())
        {
            return StartSpinAttackAbility();
        }

        if (IsPlayerInlineHorizontally())
        {
            return StartHorizontalChargeAbility();
        }

        if (IsPlayerInlineVertically())
        {
            return IsPlayerAbove() ? StartUpChargeAbility() : StartDownChargeAbility();
        }

        if (!IsPlayerNearby())
        {
            return StartBasicSlamAbility();
        }
        
        return null;
    }

    private IEnumerator GetSecondPhaseAbility()
    {
        if (IsPlayerNearby())
        {
            int randomIndex = UnityEngine.Random.Range(0,3);
            return randomIndex == 0 ? StartSpinAttackAbility() : StartChargeAttackAbility();
        }

        if (IsPlayerInlineHorizontally())
        {
            return StartHorizontalChargeAbility();
        }

        if (IsPlayerInlineVertically())
        {
            return IsPlayerAbove() ? StartUpChargeAbility() : StartDownChargeAbility();
        }

        if (!IsPlayerNearby())
        {
            int random = GetRandomNumberWithConsecutiveLimit(1, 3, 3);
            return random == 1 ? StartJumpSlamAbility() : StartBasicSlamAbility();
        }

        return null;
    }

    private IEnumerator GetAbilityTesterAbility()
    {
        switch (_loopAbility)
        {
            case Ability.UpCharge:
                return StartUpChargeAbility();
            case Ability.DownCharge:
                return StartDownChargeAbility();
            case Ability.HorizontalCharge:
                return StartHorizontalChargeAbility();
            case Ability.SpinAttack:
                return StartSpinAttackAbility();
            case Ability.ChargeAttack:
                return StartChargeAttackAbility();
            case Ability.BasicSlam:
                return StartBasicSlamAbility();
            case Ability.JumpSlam:
                return StartJumpSlamAbility();
            default:
                return null;
        }
    }

    private IEnumerator StartPhaseTwo()
    {
        yield return StartJumpGroundSlamAbility();
        yield return Helper.GetWaitForSeconds(_timeVarianceCompensationDelay);
    
        StartCoroutine(LoopAbility(GetSecondPhaseAbility));
    }

    private void StopCurrentAbility()
    {
        StopAllCoroutines();
        DeactivateAllAttackCollider();
    }


    private IEnumerator StartJumpGroundSlamAbility()
    {
        yield return _jumpGroundSlamAbility.JumpGroundSlam(_animator);
    }

    private IEnumerator StartUpChargeAbility()
    {
        yield return _upChargeAbility.UpCharge(_player, _animator);
    }

    private IEnumerator StartDownChargeAbility()
    {
        yield return _downChargeAbility.DownCharge(_player, _animator);
    }

    private IEnumerator StartHorizontalChargeAbility()
    {
        yield return _horizontalChargeAbility.HorizontalCharge(_player, _animator);
    }

    private IEnumerator StartSpinAttackAbility()
    {
        yield return _spinAttackAbility.SpinAttack(_animator);
    }
    
    private IEnumerator StartChargeAttackAbility()
    {
        yield return _chargeAttackAbility.ChargeAttack(_animator);
    }
    
    private IEnumerator StartJumpSlamAbility()
    {
        yield return _jumpSlamAbility.JumpSlam(_player, _animator);
    }
    
    private IEnumerator StartBasicSlamAbility()
    {
        yield return _basicSlamAbility.BasicSlam(_player, _animator);
    }


    private int GetRandomNumberWithConsecutiveLimit(int min, int max, int consecutiveLimit)
    {
        int random = UnityEngine.Random.Range(min, max);
        if (random == _lastRandomResult)
        {
            _consecutiveCount++;
        }
        else
        {
            _consecutiveCount = 0;
        }
        if (_consecutiveCount > consecutiveLimit)
        {
            while (random == _lastRandomResult)
            {
                random = UnityEngine.Random.Range(min, max);
            }
        }
        _lastRandomResult = random;
        return random;
    }

    private void DeactivateAllAttackCollider()
    {
        foreach (GameObject collider in _attackColliderArray)
        {
            collider.SetActive(false);
        }
    }

    protected void SetFacingDirection()
    {
        if (IsPlayerToRight())
        {
            _lookOrientation.SetFacingDirection(Vector2.right);
        }

        if (IsPlayerToLeft())
        {
            _lookOrientation.SetFacingDirection(Vector2.left);
        }
    }







#region PlayerToBossPositionInfo
    protected bool IsPlayerAbove()
    {
        return transform.position.y < _player.transform.position.y;
    }

    protected bool IsPlayerBelow()
    {
        return transform.position.y > _player.transform.position.y;
    }

    protected bool IsPlayerToRight()
    {
        return transform.position.x < _player.transform.position.x;
    }

    protected bool IsPlayerToLeft()
    {
        return transform.position.x > _player.transform.position.x;
    }

    protected bool IsPlayerInlineVertically()
    {
        return Mathf.Approximately(transform.position.x, _player.transform.position.x) ;
    }

    protected bool IsPlayerInlineHorizontally()
    {
        return Mathf.Approximately(transform.position.y, _player.transform.position.y);
    }

    protected bool IsPlayerNearby()
    {
        return Vector2.Distance(transform.position, _player.transform.position) < 1.5f;
    }
#endregion

}
