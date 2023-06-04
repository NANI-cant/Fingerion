using UnityEngine;

namespace Shop.Impl {
    [CreateAssetMenu(fileName = "ItemData", menuName = "ItemData")]
    public class ItemData : ScriptableObject, IItemData {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Sprite _ring;
        [SerializeField] private int _price;

        public int Id => _id;
        public Sprite Icon => _icon;
        public int Price => _price;
        public Sprite Ring => _ring;
    }
}