using System;
using Architecture.Services.Gameplay;
using TMPro;
using UnityEngine;

namespace UI {
    public class BankView: MonoBehaviour {
        [SerializeField] private TMP_Text _value;
        
        private IBankService _bankService;

        public void Construct(IBankService bankService) {
            _bankService = bankService;
            UpdateUI();
            
            _bankService.Modified += UpdateUI;
        }

        private void OnDestroy() {
            _bankService.Modified -= UpdateUI;
        }

        private void UpdateUI() {
            _value.text = _bankService.Amount.ShorterString();
        }
    }
}