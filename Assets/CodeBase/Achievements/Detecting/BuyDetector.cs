using Architecture.Services.Gameplay;

namespace Achievements.Detecting {
    public class BuyDetector {
        private readonly IAchievementService _achievementService;
        private readonly IBankService _bankService;
        private int _savedAmount;

        public BuyDetector(
            IAchievementService achievementService,
            IBankService bankService
        ) {
            _achievementService = achievementService;
            _bankService = bankService;

            _savedAmount = _bankService.Amount;
            _bankService.Modified += TryDetect;
        }

        private void TryDetect() {
            if (_bankService.Amount < _savedAmount) {
                _achievementService.Progress.Buy++;
                _achievementService.TryCollect();
            }

            _savedAmount = _bankService.Amount;
        }
    }
}