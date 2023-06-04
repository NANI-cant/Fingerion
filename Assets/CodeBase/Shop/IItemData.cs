using UnityEngine;

namespace Shop {
    public interface IItemData {
        int Id { get; }
        Sprite Icon { get; }
        int Price { get; }
        Sprite Ring { get; }
    }
}