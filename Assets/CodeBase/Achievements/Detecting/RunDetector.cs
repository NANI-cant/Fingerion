  using Architecture.Services.AssetProviding;
  using Architecture.Services.Gameplay;
  using Architecture.Services.General;
  using Zenject;

  namespace Achievements.Detecting {
     public class RunDetector: ITickable{
         private readonly IAchievementService _achievementService;
         private readonly IMetricProvider _metricProvider;
         private readonly ITimeProvider _timeProvider;

         private float passedWay = 0;
         private bool _detecting;

         public RunDetector(
             IAchievementService achievementService,
             IMetricProvider metricProvider,
             ITimeProvider timeProvider
         ) {
             _achievementService = achievementService;
             _metricProvider = metricProvider;
             _timeProvider = timeProvider;
         }

         public void Start() => _detecting = true;
         public void Stop() => _detecting = false;

         public void Tick() {
             if(!_detecting) return;

             passedWay += _metricProvider.PlayerMetric.Speed * _timeProvider.DeltaTime;
             if (passedWay >= 1) {
                 _achievementService.Progress.Run += (int) passedWay;
                 _achievementService.TryCollect();

                 passedWay -= (int) passedWay;
             }
         }
     }
}