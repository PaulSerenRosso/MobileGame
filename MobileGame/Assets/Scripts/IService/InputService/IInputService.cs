using System;

namespace Service.Inputs
{
    public interface IInputService : IService
    {
        void AddSwipe(SwipeSO swipeSo, Action<Swipe> successEvent);
        void RemoveSwipe(Swipe swipe);
    }
}