using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using Demyth.Gameplay;
using Core;
using MoreMountains.Tools;
using System.Threading.Tasks;

public class PetraPreCombatCutscene : MonoBehaviour
{
    
    [SerializeField] private float _firstCutsceneStartDelay;
    [SerializeField] private float _secondCutsceneStartDelay;
    [Space]
    [SerializeField] private DialogueSystemTrigger _dialogueSystemTrigger;
    [SerializeField] private PetraCombatBehaviour _petraCombatBehaviour;

    private GameInputController _gameInputController;
    private CameraController _cameraController;
    private MusicController _musicController;
    private Player _player;

    private void Awake()
    {
        _gameInputController = SceneServiceProvider.GetService<GameInputController>();
        _cameraController = SceneServiceProvider.GetService<CameraController>();
        _musicController = SceneServiceProvider.GetService<MusicController>();
        _player = SceneServiceProvider.GetService<PlayerManager>().Player;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.collider.CompareTag("Player"))
        {
            StartCoroutine(StartPreDialogueCutscene());
        }
    }

    public void OnConversationEnd()
    {
        StartCoroutine(StartPostDialogueCutscene());
    }

    private IEnumerator StartPreDialogueCutscene()
    {
        // SEQUENCE 1
        // fade out level bgm
        // disable player input
        _musicController.StartFadeCurrentMusic(10f, 0f);
        _gameInputController.DisablePlayerInput();
        yield return new WaitForSeconds(_firstCutsceneStartDelay);
        
        // SEQUENCE 2
        // move camera up
        _cameraController.DOMoveYCamera(10f, 1f, Ease.InOutCubic);
        yield return new WaitForSeconds(1f);
        
        // SEQUENCE 3
        // initiate dialogue
        _dialogueSystemTrigger.OnUse();
    }

    private IEnumerator StartPostDialogueCutscene()
    {
        // SEQUENCE 4
        yield return new WaitForSeconds(_secondCutsceneStartDelay);

        // SEQUENCE 5
        // disable petra idle
        // enable petra combat
        // move camera down
        // enable player input
        // give ara pan
        // disable cutscene object
        _petraCombatBehaviour.InitiateCombat();
        _cameraController.DOMoveYCamera(0f, 1f, Ease.InOutQuad);
        _gameInputController.EnablePlayerInput();
        _player.UsePan = true;
        // _musicController.StartPetraBossFightMusic();
        gameObject.SetActive(false);
    }
}
