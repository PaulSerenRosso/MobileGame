using Actions;
using Service.UI;

public class UltimatePlayerAction : PlayerAction
{
    public override bool IsInAction { get; }

    public override void MakeAction()
    {
        Vibration.Vibrate(100);
        MakeActionEvent?.Invoke();
    }

    public override void SetupAction(params object[] arguments)
    {
        
    }
}