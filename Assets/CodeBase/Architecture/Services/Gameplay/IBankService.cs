using System;

namespace Architecture.Services.Gameplay {
    public interface IBankService {
        public int Amount { get; }

        public bool CanSpend(int price);
        public void Spend(int price);
        public void Earn(int sum);
        event Action Modified;
    }
}