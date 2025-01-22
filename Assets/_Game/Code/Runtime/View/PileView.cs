using UnityEngine;
using Random = UnityEngine.Random;

namespace MatchHotel.View
{
    public class PileView : MonoBehaviour
    {
        [SerializeField] private BoxCollider pileBoundsCollider;

        public void AddItem(ItemView itemView)
        {
            var bounds = pileBoundsCollider.bounds;
            var min = bounds.min;
            var max = bounds.max;
            var x = Random.Range(min.x, max.x);
            var y = Random.Range(min.y, max.y);
            var z = Random.Range(min.z, max.z);
            var pos  = new Vector3(x, y, z);
            var tr = itemView.transform;
            tr.SetParent(transform);
            tr.position = pos;
            itemView.GoDynamic();
        }

        public void RemoveItem(ItemView itemView)
        {
            itemView.GoKinematic();
        }
    }
}
