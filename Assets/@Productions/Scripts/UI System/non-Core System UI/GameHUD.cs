﻿using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using DG.Tweening;
using Sirenix.OdinInspector;
using echo17.Signaler.Core;
using Demyth.Gameplay;
using Core;

namespace Demyth.UI
{
    public class GameHUD : MonoBehaviour, ISubscriber
    {
        [Title("Health/Shield Bar Parameter")]
        [SerializeField] private float _barChangeDuration = 0.5f;
        [SerializeField] private float _barPositionRange = 205f;

        [Title("Components")]
        [SerializeField] private RectTransform healthBarTransform;
        [SerializeField] private RectTransform shieldBarTransform;
        [SerializeField] private Image healthPotionEmptyImage;
        [SerializeField] private Image senterLightOnImage;

        private GameObject _playerObject;
        private Player _player;
        private HealthPotion _playerHealthPotion;
        private Health _playerHealth;
        private Shield _playerShield;

        private float _yPositionAtZeroHealth;
        private float _yPositionAtZeroShield;

        private void Awake()
        {
            // Signaler.Instance.Subscribe<PlayerSpawnEvent>(this, OnPlayerSpawned);
            // Signaler.Instance.Subscribe<PlayerDespawnEvent>(this, OnPlayerDespawned);
            GetHealthBarPositionAtZeroShield();
            GetShieldBarPositionAtZeroHealth();
        }

        // this object is not active when the signaler is broadcasting
        private bool OnPlayerSpawned(PlayerSpawnEvent signal)
        {
            _playerObject = signal.Player;
            _player = _playerObject.GetComponent<Player>();
            _playerHealthPotion = _playerObject.GetComponent<HealthPotion>();
            _playerShield = _playerObject.GetComponent<Shield>();
            _playerHealth = _playerObject.GetComponent<Health>();

            _player.OnSenterToggle += Player_OnSenterToggle;
            _playerHealthPotion.OnPotionAmountChanged += PlayerHealthPotion_OnUsePotion;
            _playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
            _playerShield.OnShieldAmountChanged += PlayerShield_OnShieldAmountChanged;

            return true;
        }

        private bool OnPlayerDespawned(PlayerDespawnEvent signal)
        {
            // _player.OnSenterToggle -= Player_OnSenterToggle;
            // _playerHealthPotion.OnPotionAmountChanged -= PlayerHealthPotion_OnUsePotion;
            // _playerHealth.OnHealthChanged -= PlayerHealth_OnHealthChanged;
            // _playerShield.OnShieldAmountChanged -= PlayerShield_OnShieldAmountChanged;

            return true;
        }

        private void OnEnable() 
        {
            if (_player == null)
            {
                _player = SceneServiceProvider.GetService<PlayerManager>().Player;
                _playerHealthPotion = _player.GetComponent<HealthPotion>();
                _playerShield = _player.GetComponent<Shield>();
                _playerHealth = _player.GetComponent<Health>();

                _player.OnSenterToggle = null;
                _playerHealthPotion.OnPotionAmountChanged = null;
                _playerHealth.OnHealthChanged = null;
                _playerShield.OnShieldAmountChanged = null;

                _player.OnSenterToggle += Player_OnSenterToggle;
                _playerHealthPotion.OnPotionAmountChanged += PlayerHealthPotion_OnUsePotion;
                _playerHealth.OnHealthChanged += PlayerHealth_OnHealthChanged;
                _playerShield.OnShieldAmountChanged += PlayerShield_OnShieldAmountChanged;
            }
        }

        private void PlayerHealth_OnHealthChanged()
        {
            UpdateHelathBar();
        }

        private void PlayerShield_OnShieldAmountChanged()
        {
            UpdateShieldBar();
        }

        private void UpdateHelathBar()
        {
            var healthAmountPercentage = _playerHealth.GetHealthPercentage();
            var newHealthPositionY = _yPositionAtZeroHealth + (healthAmountPercentage * _barPositionRange);

            healthBarTransform.DOKill();
            healthBarTransform.DOLocalMoveY(newHealthPositionY, _barChangeDuration).SetEase(Ease.OutExpo);
        }

        private void UpdateShieldBar()
        {
            var shieldAmountPercentage = _playerShield.GetShieldPercentage();
            var newShieldPositionY = _yPositionAtZeroShield + (shieldAmountPercentage * _barPositionRange);

            shieldBarTransform.DOKill();
            shieldBarTransform.DOLocalMoveY(newShieldPositionY, _barChangeDuration).SetEase(Ease.OutExpo);
        }

        private void Player_OnSenterToggle(bool senterState)
        {
            senterLightOnImage.gameObject.SetActive(senterState);
        }

        private void PlayerHealthPotion_OnUsePotion(int healthPotionAmount)
        {
            if (healthPotionAmount == 0)
            {
                healthPotionEmptyImage.gameObject.SetActive(true);
            }
            else
            {
                healthPotionEmptyImage.gameObject.SetActive(false);
            }
        }

        private void GetHealthBarPositionAtZeroShield()
        {
            _yPositionAtZeroHealth = healthBarTransform.localPosition.y - _barPositionRange;
        }

        private void GetShieldBarPositionAtZeroHealth()
        {
            _yPositionAtZeroShield = shieldBarTransform.localPosition.y - _barPositionRange;
        }
    }
}
