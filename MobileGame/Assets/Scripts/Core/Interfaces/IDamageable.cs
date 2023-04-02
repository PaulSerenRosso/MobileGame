using System;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(float amount, Vector3 posToCheck = new());
        public event Action ChangeHealth;
    }
}