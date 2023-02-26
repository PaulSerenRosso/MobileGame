using System;
using System.Collections.Generic;
using Action;
using UnityEngine;

namespace Player.Handler
{
    public abstract class PlayerHandler<AC> : MonoBehaviour where AC : IAction
    {
        [SerializeField] protected AC _action;
        protected List<Func<bool>> _conditions = new();

        protected void TryMakeAction()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.Invoke())
                {
                    return;
                }
            }

            InitializeAction();
            _action.MakeAction();
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
    }
}