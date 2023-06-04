using System;
using System.Collections.Generic;
using Actions;
using UnityEngine;

namespace Player.Handler
{
    public abstract class PlayerHandler : MonoBehaviour
    {
        protected abstract PlayerAction GetAction();
        protected List<Func<bool>> _conditions = new();
        
        protected virtual void TryMakeAction(params object[] args)
        {
            foreach (var condition in _conditions)
            {
                if (!condition.Invoke())
                {
                    return;
                }
            }

            InitializeAction();
            GetAction().MakeAction();
        }

        public void AddCondition(Func<bool> conditionToAdd)
        {
            _conditions.Add(conditionToAdd);
        }

        public void RemoveCondition(Func<bool> conditionToRemove)
        {
            _conditions.Remove(conditionToRemove);
        }

        public abstract void InitializeAction();

        public abstract void Setup(params object[] arguments);

        public virtual void Unlink()
        {
            GetAction().UnlinkAction();
        }
    }
}