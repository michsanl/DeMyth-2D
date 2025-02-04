﻿using UnityEngine;
using Core;
using Demyth.Gameplay;
using PixelCrushers;
using System;

public class GameManager : SceneService
{

    [SerializeField] private GameSettingsSO _gameSettingsSO;
    [SerializeField] private GameObject[] _levelGateArray;

    private GameStateService _gameStateService;
    private GameInputController _gameInputController;
    private GameInput _gameInput;

    private void Awake()
    {
        _gameStateService = SceneServiceProvider.GetService<GameStateService>();
        _gameInputController = SceneServiceProvider.GetService<GameInputController>();
        _gameInput = _gameInputController.GameInput;

        _gameStateService[GameState.Pause].onEnter += Pause_OnEnter;
        _gameStateService[GameState.Pause].onExit += Pause_OnExit;

        _gameInput.OnPausePerformed.AddListener(GameInput_OnPausePerformed);

        CreateVanillaSaveFile();
        Application.runInBackground = true; // So music wont pause
    }

    private void Start()
    {
        SetupLevelGate();
    }

    public void SaveGameplayProgress()
    {
        SaveSystem.SaveToSlot(1);
    }

    public void SetGameStateToGameplay()
    {
        _gameStateService.SetState(GameState.Gameplay);
    }

    public void SetGameStateToGameOver()
    {
        _gameStateService.SetState(GameState.GameOver);
    }

    // Saving on the start of the scene, before loading anything, to create vanilla save file
    private static void CreateVanillaSaveFile()
    {
        SaveSystem.SaveToSlot(0);
    }

    private void Pause_OnEnter(GameState state)
    {
        Pause();
    }

    private void Pause_OnExit(GameState state)
    {
        UnPause();
    }

    private void GameInput_OnPausePerformed()
    {
        ToggleGameStatePause();
    }

    private void ToggleGameStatePause()
    {
        if (_gameStateService.CurrentState != GameState.Pause)
        {
            _gameStateService.SetState(GameState.Pause);
        }
        else
        {
            _gameStateService.SetState(GameState.Gameplay);
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void UnPause()
    {
        Time.timeScale = 1f;
    }

    private void SetupLevelGate()
    {
        foreach (var levelGate in _levelGateArray)
        {
            levelGate.SetActive(_gameSettingsSO.UnlockAllGateOnStart);
        }
    }
}
