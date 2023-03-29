using System;

namespace Interfaces
{
    public interface IDamageable
    {
        
        
        public void TakeDamage(float amount);

        public event Action ChangeHealth;
    }
}