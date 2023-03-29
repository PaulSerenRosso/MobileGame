using System;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour, IDeathable, IDamageable, ILifeable
    {
        [SerializeField] private float _maxHealth;

        private float _health;

        private void Start()
        {
            _health = _maxHealth;
        }

        public void Die()
        {
            Debug.Log("PLAYER IS KO! LOSER!");
        }

        public void TakeDamage(float amount)
        {
            if (_health - amount <= 0)
            {
                _health = 0;
                ChangeHealth?.Invoke();
                Die();
            }
            else
            {
                Debug.Log("Player took : " + amount);
                Debug.Log("Player have : " + _health);
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
    }
}