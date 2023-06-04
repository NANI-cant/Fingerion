using System.Runtime.InteropServices;
using Architecture.Services.Gameplay;

namespace Ads.Impl {
    public class YandexAdService : IAdService {
        private readonly IBankService _bankService;

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void ShowYandexAd();
        [DllImport("__Internal")]
        private static extern void ShowYandexRewardedAd();
#endif
        
        public YandexAdService(IBankService bankService) {
            _bankService = bankService;
        }

        public void ShowInterstitial() {
#if UNITY_WEBGL
            ShowYandexAd();
#endif
        }

        public void ShowRewarded() {
#if UNITY_WEBGL
            ShowYandexRewardedAd();
#endif      
        }

        public void GiveReward() {
            _bankService.Earn(100);
        }
    }
}