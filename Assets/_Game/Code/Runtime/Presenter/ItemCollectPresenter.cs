using MatchHotel.View;
using UnityEngine;

namespace MatchHotel.Presenter
{
    public class ItemCollectPresenter
    {
        private readonly ItemCollectView _view;
        private readonly PileView _pileView;
        private readonly InventoryPresenter _inventoryPresenter;

        public ItemCollectPresenter(ItemCollectView view, PileView pileView, InventoryPresenter inventoryPresenter)
        {
            _view = view;
            _pileView = pileView;
            _inventoryPresenter = inventoryPresenter;
            _view.OnPointerDownEvent.AddListener(OnPointerDown);
        }

        private void OnPointerDown(Vector3 pressPos)
        {
            var ray = Camera.main.ScreenPointToRay(pressPos);
            if (!Physics.Raycast(ray, out var hitInfo, 200, _view.ItemLayerMask))
            {
                return;
            }

            var itemView = hitInfo.collider.GetComponentInParent<ItemView>();
            if (!itemView)
            {
                return;
            }
            
            Debug.Log($"{itemView.name} collected.");

            _pileView.RemoveItem(itemView);
            _inventoryPresenter.AddItem(itemView);
        }

        public void Dispose()
        {
            _view.OnPointerDownEvent.RemoveListener(OnPointerDown);
        }
    }
}