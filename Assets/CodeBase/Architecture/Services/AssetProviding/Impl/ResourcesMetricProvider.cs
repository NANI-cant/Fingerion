using Achievements;
using Achievements.Impl;
using Metric;
using Metric.Impl;
using Shop;
using Shop.Impl;
using UnityEngine;

namespace Architecture.Services.AssetProviding.Impl {
    public class ResourcesMetricProvider : IMetricProvider {
        private const string PlayerPath = "Metrics/Player";
        private const string MetricsGame = "Metrics/Game";
        private const string ShopFolderPath = "Shop/";
        private const string AchievementsFolderPath = "Achievements/";

        private IItemData[] _items;
        private IAchievement[] _achievements;

        public IPlayerMetric PlayerMetric => Resources.Load<PlayerMetric>(PlayerPath);
        public IGameMetric GameMetric => Resources.Load<GameMetric>(MetricsGame);
        public IItemData[] Items => _items;
        public IAchievement[] Achievements => _achievements;

        public ResourcesMetricProvider() {
            _items = Resources.LoadAll<ItemData>(ShopFolderPath);
            _achievements = Resources.LoadAll<Achievement>(AchievementsFolderPath);
        }
    }
}