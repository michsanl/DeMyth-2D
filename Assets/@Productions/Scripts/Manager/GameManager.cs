 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomTools.Core;
using Mono.Cecil;

public class GameManager : SceneService
{
    public Action OnGamePaused;
    public Action OnGameUnpaused;
    public bool IsGamePaused => isGamePaused;

    private bool isGamePaused;

    protected override void OnInitialize()
    {
    }

    protected override void OnActivate()
    {
        Context.GameInput.OnPausePerformed += GameInput_OnPausePerformed;
    }

    private void OnDestroy() 
    {
        Context.GameInput.OnPausePerformed -= GameInput_OnPausePerformed;
    }

    private void GameInput_OnPausePerformed()
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            Context.CameraShakeController.gameObject.SetActive(false);
            OnGamePaused?.Invoke();
        } else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke();
        }
    }
    
}
