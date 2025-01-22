using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace MatchHotel.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform[] slots;
        [SerializeField] private float jumpTweenDuration = 0.5f;
        [SerializeField] private float jumpTweenPower = 1000;
        [SerializeField] private float shiftJumPTweenPower = 200;
        [SerializeField] private float mergeMeetPointYDistance = 10f;
        [SerializeField] private float mergeMeetDuration = 0.5f;
        [SerializeField] private Vector3 itemTweenLocalScale = Vector3.one * 100;

        private ItemView[] _itemViews;

        private void Awake()
        {
            _itemViews = new ItemView[slots.Length];
        }

        public void AddItemToSlot(ItemView itemView, int slotIndex)
        {
            var oldItemView = GetItemAtSlot(slotIndex);
            SetItemAtSlot(itemView, slotIndex);
            PlayJumpAnim(itemView, slotIndex);
            slotIndex++;
            ShiftItemsToRight(slotIndex, oldItemView);
        }
        
        private void PlayJumpAnim(ItemView itemView, int slotIndex)
        {
            var tr = itemView.transform;
            var sequence = DOTween.Sequence();
            var parent = slots[slotIndex];
            sequence.Join(tr.DOLocalJump(Vector3.zero, jumpTweenPower, 1, jumpTweenDuration))
                .Join(tr.DOScale(itemTweenLocalScale, jumpTweenDuration))
                .Join(tr.DOLocalRotate(Vector3.zero, jumpTweenDuration))
                .Pause()
                .OnStart(() => tr.SetParent(parent));
            itemView.PlayTweenQueued(sequence);
        }

        private void ShiftItemsToRight(int slotIndex, ItemView oldItemView)
        {
            while (oldItemView && slotIndex < slots.Length)
            {
                var itemView = oldItemView;
                oldItemView = GetItemAtSlot(slotIndex);
                SetItemAtSlot(itemView, slotIndex);
                DoShiftJump(itemView, slots[slotIndex]);
                slotIndex++;
            }
        }

        private void DoShiftJump(ItemView itemView, Transform parent)
        {
            if (!itemView)
            {
                return;
            }
            
            var tween = itemView.transform.DOLocalJump(Vector3.zero, shiftJumPTweenPower, 1, jumpTweenDuration)
                .Pause()
                .OnStart(() => itemView.transform.SetParent(parent));
            itemView.PlayTweenQueued(tween);
        }

        public void MergeItems(int middleSlotIndex)
        {
            Assert.IsTrue(middleSlotIndex > 0);
            Assert.IsTrue(middleSlotIndex < slots.Length - 1);
            var l = GetItemAtSlot(middleSlotIndex - 1);
            var m = GetItemAtSlot(middleSlotIndex);
            var r = GetItemAtSlot(middleSlotIndex + 1);
            Assert.AreEqual(l.ItemID, m.ItemID);
            Assert.AreEqual(m.ItemID, r.ItemID);
            
            SetItemAtSlot(null, middleSlotIndex - 1);
            SetItemAtSlot(null, middleSlotIndex);
            SetItemAtSlot(null, middleSlotIndex + 1);
            
            ShiftRemainingItems(middleSlotIndex); 
            PlayMergeAnim(middleSlotIndex, m, l, r);
        }
        
        private void ShiftRemainingItems(int middleSlotIndex)
        {
            for (int i = middleSlotIndex + 2; i < slots.Length; i++)
            {
                var item = GetItemAtSlot(i);
                if (!item)
                {
                    break;
                }

                SetItemAtSlot(item, i - 3);
                SetItemAtSlot(null, i);
                DoShiftJump(item, slots[i - 3]);
            }
        }

        private void PlayMergeAnim(int middleSlotIndex, ItemView m, ItemView l, ItemView r)
        {
            var pos = slots[middleSlotIndex].TransformPoint(Vector3.up * mergeMeetPointYDistance);

            var mTween = m.transform.DOMove(pos, mergeMeetDuration)
                .Pause()
                .OnStart(() => m.transform.SetParent(null))
                .OnComplete(() => Destroy(m.gameObject));
            var lTween = l.transform.DOMove(pos, mergeMeetDuration)
                .Pause()
                .OnStart(() => l.transform.SetParent(null))
                .OnComplete(() => Destroy(l.gameObject));
            var rTween = r.transform.DOMove(pos, mergeMeetDuration)
                .Pause()
                .OnStart(() => r.transform.SetParent(null))
                .OnComplete(() => Destroy(r.gameObject));

            var sequence = DOTween.Sequence();
            sequence.Join(mTween);
            sequence.Join(lTween);
            sequence.Join(rTween);
            r.PlayTweenQueued(sequence);
        }

        private ItemView GetItemAtSlot(int slotIndex)
        {
            return _itemViews[slotIndex];
        }

        private void SetItemAtSlot(ItemView item, int slotIndex)
        {
            _itemViews[slotIndex] = item;
        }
    }
}