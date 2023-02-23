using System;
using Environnement.MoveGrid;
using UnityEngine;
using Exception = System.Exception;
using Object = UnityEngine.Object;

[Serializable]
public class Blackboard
{
    public EnemyValueObject[] EnemyValueObjects;
    public ExternValueObject[] ExternValueObjects;

    public Blackboard(Transform playerTransform, ITickeableService tickeableService, EnvironmentGridManager environmentGridManager)
    {
        for (int i = 0; i < ExternValueObjects.Length; i++)
        {
            switch (ExternValueObjects[i].Type)
            {
                case Enums.TreeExternValues.PlayerTransform :
                {
                    ExternValueObjects[i].Obj = playerTransform;
                    break;
                }
                case Enums.TreeExternValues.EnvironmentGridManager :
                {
                    ExternValueObjects[i].Obj =environmentGridManager;
                    break;
                }
                case Enums.TreeExternValues.ITickService :
                {
                    ExternValueObjects[i].Obj = tickeableService; 
                    break;
                }
            }
        }
    }
    public Object GetEnemyValueObject(Enums.TreeEnemyValues type)
    {
        for (int i = 0; i < EnemyValueObjects.Length; i++)
        {
            if (EnemyValueObjects[i].Type == type)
                return EnemyValueObjects[i].Obj;
        }
        throw new NullReferenceException();
    }

    public System.Object GetExternValueObjects(Enums.TreeExternValues type)
    {
        for (int i = 0; i < ExternValueObjects.Length; i++)
        {
            if ( ExternValueObjects[i].Type == type)
                return ExternValueObjects[i].Obj;
        }
        throw new NullReferenceException();
    }

}
