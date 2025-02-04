using System;
using Core;
using Demyth.Gameplay;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers;
using DG.Tweening;
using Lean.Pool;

public class Level2RestartHandler : MonoBehaviour
{
    
    private GameStateService _gameStateService;
    
    private void Awake()
    {
        _gameStateService = SceneServiceProvider.GetService<GameStateService>();
    }

    private void OnEnable()
    {
        _gameStateService[GameState.Gameplay].onEnter += GameStateGamePlay_OnEnter;
    }

    private void OnDisable() 
    {
        _gameStateService[GameState.Gameplay].onEnter -= GameStateGamePlay_OnEnter;
    }

    private void GameStateGamePlay_OnEnter(GameState state)
    {
        if (_gameStateService.PreviousState == GameState.GameOver)
        {
            ResetLevel();
        }
    }

    public void ResetLevel()
    {
        DOTween.CompleteAll();
        SaveSystem.LoadFromSlot(1);
    }
}
