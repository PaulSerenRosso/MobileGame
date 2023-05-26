using System.Collections.Generic;
using BehaviorTree.SO.Actions;
using UnityEngine;

namespace BehaviorTree.Nodes.Actions
{
    public class TaskActivationFXNode : ActionNode
    {
        private TaskActivationFXNodeSO _so;
        private TaskActivationFXNodeDataSO _data;
        private GameObject[] _gameObjects;
        private Transform _enemyTransform;

        public override void SetNodeSO(NodeSO nodeSO)
        {
            _so = (TaskActivationFXNodeSO)nodeSO;
            _data = (TaskActivationFXNodeDataSO)_so.Data;
        }

        public override NodeSO GetNodeSO()
        {
            return _so;
        }

        public override void Evaluate()
        {
            base.Evaluate();
            foreach (var variableGameObject in _gameObjects)
            {
                variableGameObject.gameObject.SetActive(!variableGameObject.gameObject.activeSelf);
                if (_data.IsColorChanged)
                {
                    var mainModule = variableGameObject.GetComponent<ParticleSystem>().main;
                    mainModule.startColor = _data.ParticleColor;
                }
                
                if (_data.IsDirectionChanged)
                {
                    variableGameObject.transform.forward = _enemyTransform.TransformDirection(new Vector3( _data.ParticleDirection.x,0,_data.ParticleDirection.y));
                }
            }

            State = BehaviorTreeEnums.NodeState.SUCCESS;
            ReturnedEvent?.Invoke();
        }

        public override void SetDependencyValues(
            Dictionary<BehaviorTreeEnums.TreeExternValues, object> externDependencyValues,
            Dictionary<BehaviorTreeEnums.TreeEnemyValues, object> enemyDependencyValues)
        {
            _enemyTransform = (Transform)enemyDependencyValues[BehaviorTreeEnums.TreeEnemyValues.Transform];
            _gameObjects = new GameObject[enemyDependencyValues.Count-1];
            int count = 0;
            foreach (var enemyDependencyValue in enemyDependencyValues)
            {
                if (count == enemyDependencyValues.Count - 1) return;
                _gameObjects[count] = (GameObject)enemyDependencyValue.Value;
                count++;
            }
        }

        public override ActionNodeDataSO GetDataSO()
        {
            return _data;
        }
    }
}