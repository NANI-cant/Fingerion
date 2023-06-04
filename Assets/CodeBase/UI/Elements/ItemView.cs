using System;
using Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class ItemView: MonoBehaviour {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private GameObject _activeLabel;
        [SerializeField] private TMP_Text _price;

        public IItemData ItemData { get; private set; }

        public event Action<IItemData> Bought;
        public event Action<IItemData> Selected;

        public void Construct(IItemData itemData) {
            ItemData = itemData;
            _icon.sprite = itemData.Icon;
            _price.text = itemData.Price.ShorterString();
            
            _buyButton.onClick.AddListener(Buy);
            _selectButton.onClick.AddListener(Select);
        }

        private void OnDestroy() {
            _buyButton.onClick.RemoveListener(Buy);
            _selectButton.onClick.RemoveListener(Select);
        }

        public void SetAvailable() {
            _buyButton.gameObject.SetActive(true);
            _buyButton.interactable = true;
            _selectButton.gameObject.SetActive(false);
            _activeLabel.SetActive(false);
            _price.color = Color.black;
        }

        public void SetActive() {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(false);
            _activeLabel.SetActive(true);
        }

        public void SetBought() {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
            _activeLabel.SetActive(false);
        }

        public void SetUnAvailable() {
            _buyButton.gameObject.SetActive(true);
            _buyButton.interactable = false;
            _selectButton.gameObject.SetActive(false);
            _activeLabel.SetActive(false);
            _price.color = Color.red;
        }

        private void Select() => Selected?.Invoke(ItemData);
        private void Buy() => Bought?.Invoke(ItemData);
    }
}