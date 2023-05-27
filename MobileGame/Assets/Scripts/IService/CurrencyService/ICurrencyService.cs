using System;

namespace Service.Currency
{
    public interface ICurrencyService : IService
    {
        int GetXP();
        int GetCoins();
        void AddCoins(int amount);
        void RemoveCoins(int amount);
        void AddXP(int amount);
        event Action<int> OnAddCoins;
        event Action<int> OnRemoveCoins;
        event Action<int> OnAddXP;
    }
}