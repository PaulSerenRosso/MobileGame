using Actions;

public class UltimatePlayerAction : PlayerAction
{
    public override bool IsInAction { get; }

    public override void MakeAction()
    {
        MakeActionEvent?.Invoke();
    }

    public override void SetupAction(params object[] arguments)
    {
        
    }
}