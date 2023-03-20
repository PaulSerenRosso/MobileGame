namespace Action
{
    public interface IAction
    {
        public bool IsInAction { get; }
        public void MakeAction();
        public void SetupAction(params object[] arguments);
        public event System.Action MakeActionEvent;
    }
}