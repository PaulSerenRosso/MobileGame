using UnityEngine;

public interface IHypeable
{
    public bool TryDecreaseHypeEnemy(float amount, Vector3 posToCheck, Transform particleTransform);
}