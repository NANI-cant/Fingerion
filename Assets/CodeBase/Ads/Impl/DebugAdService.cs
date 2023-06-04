using Architecture.Services.Gameplay;
using UnityEngine;

namespace Ads.Impl {
    public class DebugAdService : IAdService {
        private readonly IBankService _bankService;

        public DebugAdService(IBankService bankService) {
            _bankService = bankService;
        }
        
        public void ShowInterstitial() {
            Debug.Log("Interstitial AD");
        }

        public void ShowRewarded() {
            Debug.Log("Rewarded AD");
            _bankService.Earn(100);
        }
    }
}