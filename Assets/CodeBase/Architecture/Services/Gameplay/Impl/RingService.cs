using System;
using System.Linq;
using Architecture.Services.AssetProviding;
using Architecture.Services.PersistentProgress;
using Shop;
using UnityEngine;

namespace Architecture.Services.Gameplay.Impl {
    public class RingService : IRingService {
        public IItemData ActiveRing { get; private set; }
        
        public event Action Modified;

        public RingService(
            IPersistentProgressService persistentProgressService,
            IMetricProvider metricProvider
        ) {
            ActiveRing = metricProvider.Items.FirstOrDefault(data => data.Id == persistentProgressService.Progress.SelectedItem);
        }
        
        public void SetItem(IItemData itemData) {
            Debug.Log($"Selected {itemData.Id}");
            ActiveRing = itemData;
            Modified?.Invoke();
        }
    }
}