using System.Collections;
using Actions;
using UnityEngine;

public class UltimatePlayerAction : PlayerAction
{
    public override bool IsInAction { get; }

    public override void MakeAction()
    {
        // TODO : Add cutscenes and call the event at the end
        StartCoroutine(GenerateCutscene());
        MakeActionEvent?.Invoke();
    }

    private IEnumerator GenerateCutscene()
    {
        yield return new WaitForSeconds(5);
    }

    public override void SetupAction(params object[] arguments)
    {
        
    }
}