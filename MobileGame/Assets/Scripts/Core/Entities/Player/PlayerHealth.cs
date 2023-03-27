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
                Die();
            }
            else _health -= amount;
        }

        public void GainHealth(float amount)
        {
            if (_health >= _maxHealth) return;
            if (_health + amount >= _maxHealth) _health = _maxHealth;
            else _health += amount;
        }
    }
}