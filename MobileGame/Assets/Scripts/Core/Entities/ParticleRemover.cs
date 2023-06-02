using Service.Fight;
using UnityEngine;

public class ParticleRemover : MonoBehaviour
{
    public void Initialize(IFightService fightService)
    {
        fightService.ActivatePauseEvent += () => gameObject.SetActive(false);
    }
}
