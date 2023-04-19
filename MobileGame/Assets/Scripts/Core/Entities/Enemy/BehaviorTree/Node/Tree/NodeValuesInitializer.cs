using System;
using Environment.MoveGrid;
using Player.Handler;
using Service;
using Service.Hype;
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
            GridManager gridManager, IPoolService poolService, IHypeService hypeService)
        {
            for (int i = 0; i < ExternValueObjects.Length; i++)
            {
                switch (ExternValueObjects[i].Type)
                {
                    case BehaviorTreeEnums.TreeExternValues.PlayerTransform:
                    {
                        ExternValueObjects[i].Obj = playerTransform;
                        break;
                    }
                    case BehaviorTreeEnums.TreeExternValues.GridManager:
                    {
                        ExternValueObjects[i].Obj = gridManager;
                        break;
                    }
                    case BehaviorTreeEnums.TreeExternValues.TickManager:
                    {
                        ExternValueObjects[i].Obj = tickeableService.GetTickManager;
                        break;
                    }
                    case BehaviorTreeEnums.TreeExternValues.PlayerHandlerMovement:
                    {
                        ExternValueObjects[i].Obj = playerTransform.GetComponent<PlayerMovementHandler>();
                        break;
                    }
                    case BehaviorTreeEnums.TreeExternValues.PoolService:
                    {
                        ExternValueObjects[i].Obj = poolService;
                        break;
                    }
                    case BehaviorTreeEnums.TreeExternValues.HypeService:
                    {
                        ExternValueObjects[i].Obj = hypeService;
                        break;
                    }
                }
            }
        }

        public Object GetEnemyValueObject(BehaviorTreeEnums.TreeEnemyValues type)
        {
            for (int i = 0; i < EnemyValueObjects.Length; i++)
            {
                if (EnemyValueObjects[i].Type == type)
                    return EnemyValueObjects[i].Obj;
            }

            throw new NullReferenceException();
        }

        public System.Object GetExternValueObject(BehaviorTreeEnums.TreeExternValues type)
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