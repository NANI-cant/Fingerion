using System;
using Architecture.Services.PersistentProgress;
using UnityEngine;

namespace Architecture.Services.Gameplay.Impl {
    public class BankService : IBankService{
        private readonly IPersistentProgressService _persistentProgressService;
        
        public int Amount { get; private set; }

        public event Action Modified;

        public BankService(
            IPersistentProgressService persistentProgressService
        ) {
            _persistentProgressService = persistentProgressService;
            
            if (_persistentProgressService.Progress == null) {
                _persistentProgressService.Loaded += InitializeDelayed;
            }
            else {
                Amount = _persistentProgressService.Progress.BankAmount;   
            }
        }

        private void InitializeDelayed() {
            Amount = _persistentProgressService.Progress.BankAmount;
            _persistentProgressService.Loaded -= InitializeDelayed;
        }
        
        public bool CanSpend(int price) {
            return price >= 0 && Amount >= price;
        }

        public void Spend(int price) {
            if(!CanSpend(price)) return;

            Amount -= price;
            _persistentProgressService.Progress.BankAmount = Amount;
            _persistentProgressService.Save();
            Modified?.Invoke();
        }

        public void Earn(int sum) {
            if(sum < 0) return;
            Amount += sum;
            
            _persistentProgressService.Progress.BankAmount = Amount;
            _persistentProgressService.Save();
            Modified?.Invoke();
        }
    }
}