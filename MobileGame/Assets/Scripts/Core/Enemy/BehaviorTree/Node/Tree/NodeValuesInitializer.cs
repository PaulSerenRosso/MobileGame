using System;
using Environment.MoveGrid;
using Player.Handler;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BehaviorTree.Trees
{
    [Serializable]
    public class NodeValuesInitializer
    {
        public EnemyValueObject[] EnemyValueObjects;
        public ExternValueObject[] ExternValueObjects;

        public void Setup(Transform playerTransform, ITickeableService tickeableService,
            EnvironmentGridManager environmentGridManager)
        {
            for (int i = 0; i < ExternValueObjects.Length; i++)
            {
                switch (ExternValueObjects[i].Type)
                {
                    case BehaviourTreeEnums.TreeExternValues.PlayerTransform:
                    {
                        ExternValueObjects[i].Obj = playerTransform;
                        break;
                    }
                    case BehaviourTreeEnums.TreeExternValues.EnvironmentGridManager:
                    {
                        ExternValueObjects[i].Obj = environmentGridManager;
                        break;
                    }
                    case BehaviourTreeEnums.TreeExternValues.TickManager:
                    {
                        ExternValueObjects[i].Obj = tickeableService.GetTickManager;
                        break;
                    }
                    case BehaviourTreeEnums.TreeExternValues.PlayerHandlerMovement:
                    {
                        ExternValueObjects[i].Obj = playerTransform.GetComponent<PlayerMovementHandler>();
                        break;
                    }
                }
            }
        }

        public Object GetEnemyValueObject(BehaviourTreeEnums.TreeEnemyValues type)
        {
            for (int i = 0; i < EnemyValueObjects.Length; i++)
            {
                if (EnemyValueObjects[i].Type == type)
                    return EnemyValueObjects[i].Obj;
            }

            throw new NullReferenceException();
        }

        public System.Object GetExternValueObject(BehaviourTreeEnums.TreeExternValues type)
        {
            for (int i = 0; i < ExternValueObjects.Length; i++)
            {
                if (ExternValueObjects[i].Type == type)
                    return ExternValueObjects[i].Obj;
            }

            throw new NullReferenceException();
        }
    }
}