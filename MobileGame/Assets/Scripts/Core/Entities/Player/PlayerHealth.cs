using System;
using HelperPSR.RemoteConfigs;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDeathable, IDamageable, ILifeable
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private string _remoteConfigHealthName;

        private float _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void Die()
        {
            Debug.Log("PLAYER IS KO! LOSER!");
        }

        public void TakeDamage(float amount, Vector3 posToCheck = new())
        {
            if (_health - amount <= 0)
            {
                _health = 0;
                ChangeHealth?.Invoke();
                Die();
            }
            else
            {
                _health -= amount;
                ChangeHealth?.Invoke();
            }
        }

        public event Action ChangeHealth;

        public float GetHealth()
        {
            return _health;
        }

        public void GainHealth(float amount)
        {
            if (_health >= _maxHealth) return;
            if (_health + amount >= _maxHealth)
            {
                _health = _maxHealth;
                ChangeHealth?.Invoke();
            }
            else
            {
                _health += amount;
                ChangeHealth?.Invoke();
            }
        }
        
        public void SetRemoteConfigurableValues()
        {
            _maxHealth = RemoteConfigManager.Config.GetFloat(_remoteConfigHealthName);
        }
    }
}