using System;
using Core;
using Demyth.Gameplay;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers;
using DG.Tweening;
using Lean.Pool;

public class BossLevelReset : MonoBehaviour
{

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
        // To do :
        // Reset health, shield, etc on Player & Boss
        // Load from SaveSystem to reset object position & active state
        DOTween.KillAll();

        _player.ResetUnitCondition();

        SaveSystem.LoadFromSlot(1);
    }
}
