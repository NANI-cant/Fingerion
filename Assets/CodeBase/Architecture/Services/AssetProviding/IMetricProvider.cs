using Achievements;
using Metric;
using Shop;

namespace Architecture.Services.AssetProviding {
    public interface IMetricProvider {
        IPlayerMetric PlayerMetric { get; }
        IGameMetric GameMetric { get; }
        IItemData[] Items { get; }
        IAchievement[] Achievements { get; }
    }
}