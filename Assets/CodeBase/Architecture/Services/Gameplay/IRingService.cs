using System;
using Shop;

namespace Architecture.Services.Gameplay {
    public interface IRingService {
        IItemData ActiveRing { get; }

        event Action Modified;

        void SetItem(IItemData itemData);
    }
}