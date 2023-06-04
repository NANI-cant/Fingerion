using System.Collections.Generic;
using Architecture.Services.Factories;
using Architecture.Services.Gameplay;
using Architecture.Services.PersistentProgress;
using Shop;
using UnityEngine;

namespace UI.Screens {
    public class ShopScreen:MonoBehaviour {
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private ItemView _templateItem;

        private readonly List<ItemView> _itemViews = new();
        private IBankService _bankService;
        private IPersistentProgressService _persistentProgressService;
        private IRingService _ringService;

        public void Construct(
            IItemData[] itemsData, 
            IBankService bankService,
            IPersistentProgressService persistentProgressService,
            IUIFactory uiFactory,
            IRingService ringService
        ) {
            _ringService = ringService;
            _bankService = bankService;
            _persistentProgressService = persistentProgressService;
            
            foreach (var itemData in itemsData) {
                var view = uiFactory.CreateShopItem(_templateItem, _itemsContainer, itemData);
                view.Bought += RectToBuy;
                view.Selected += ReactToSelect;
                _itemViews.Add(view);
            }
            UpdateViews();
            
            _bankService.Modified += UpdateViews;
        }

        private void ReactToSelect(IItemData data) {
            _persistentProgressService.Progress.SelectedItem = data.Id;
            _persistentProgressService.Save();
            _ringService.SetItem(data);
            UpdateViews();
        }

        private void RectToBuy(IItemData data) {
            _bankService.Spend(data.Price);
            _persistentProgressService.Progress.BuyedItems.Add(data.Id);
            ReactToSelect(data);
        }

        private void OnDestroy() {
            _bankService.Modified -= UpdateViews;
        }

        private void UpdateViews() {
            foreach (var itemView in _itemViews) {
                var data = itemView.ItemData;
                if (_persistentProgressService.Progress.BuyedItems.Contains(data.Id)) {
                    if (_persistentProgressService.Progress.SelectedItem == data.Id) {
                        itemView.SetActive();
                    }
                    else {
                        itemView.SetBought();
                    }
                }
                else {
                    if (_bankService.CanSpend(data.Price)) {
                        itemView.SetAvailable();
                    }
                    else {
                        itemView.SetUnAvailable();
                    }   
                }
            }
        }
    }
}