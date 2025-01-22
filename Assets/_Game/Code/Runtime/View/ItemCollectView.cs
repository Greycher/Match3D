using MatchHotel.Presenter;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MatchHotel.View
{
    public class ItemCollectView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private LayerMask itemLayerMask;
        
        public UnityEvent<Vector3> OnPointerDownEvent = new ();
        public LayerMask ItemLayerMask => itemLayerMask;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke(eventData.pressPosition);
        }

        private void OnDestroy()
        {
            OnPointerDownEvent.RemoveAllListeners();
        }
    }
}