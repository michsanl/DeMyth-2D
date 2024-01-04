using System;
using Core;
using Demyth.Gameplay;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers;
using DG.Tweening;
using Lean.Pool;
using UnityEngine.Rendering.Universal;

public class BossLevelReset : MonoBehaviour
{

    [SerializeField] private Light2D _globalLight;
    [SerializeField] private PillarLight[] _pillarLights;
    [SerializeField] private SriCombatEvent _sriCombatEvent;

    private Player _player;
    private Health _playerHealth;
    private GameStateService _gameStateService;
    
    private void Awake()
    {
        _gameStateService = SceneServiceProvider.GetService<GameStateService>();
        _player = SceneServiceProvider.GetService<PlayerManager>().Player;
        _playerHealth = _player.GetComponent<Health>();
    }

    private void OnEnable()
    {
        _gameStateService[GameState.Gameplay].onEnter += GameStateGamePlay_OnEnter;
        _playerHealth.OnDeath += PlayerHealth_OnDeath;
    }

    private void OnDisable() 
    {
        _gameStateService[GameState.Gameplay].onEnter -= GameStateGamePlay_OnEnter;
        _playerHealth.OnDeath -= PlayerHealth_OnDeath;
    }

    private void GameStateGamePlay_OnEnter(GameState state)
    {
        if (_gameStateService.PreviousState == GameState.GameOver)
        {
            ResetLevel();
        }
    }

    private void PlayerHealth_OnDeath()
    {
        _gameStateService.SetState(GameState.GameOver);
        LeanPool.DespawnAll();
    }

    public void ResetLevel()
    {
        DOTween.CompleteAll();

        _player.ResetUnitCondition();

        if (_sriCombatEvent != null)
        {
            _sriCombatEvent.StopAllCoroutines();
        }

        if (_globalLight != null) 
        {
            _globalLight.intensity = 1f;
        }

        if (_pillarLights.Length > 0)
        {
            foreach (var pillarLight in _pillarLights)
            {
                pillarLight.TurnOnPillarLight();
            }
        }

        SaveSystem.LoadFromSlot(1);
    }
}
