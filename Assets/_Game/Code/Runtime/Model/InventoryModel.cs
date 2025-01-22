using System;
using System.Collections.Generic;
using MatchHotel.Common;
using UnityEngine.Assertions;

namespace MatchHotel.Model
{
    public class InventoryModel
    {
        //Probably using list is more optimized here
        //Especially because of allocation, since no pooling
        //But it felt more right to use linked list since there frequent insertion and shifting
        //Even though readability of the code drastically worse :)
        public LinkedList<int> _items = new LinkedList<int>();

        public delegate void OnItemAddedAction(int itemID, int itemIndex);
        public delegate void OnItemsMergedAction(int middleItemIndex);
        public OnItemAddedAction OnItemAddedEvent;
        public OnItemsMergedAction OnItemsMergedEvent;
        
        public int LookAHeadIndex(int itemID, out LinkedListNode<int> node)
        {
            node = _items.Last;
            var itemIndex = _items.Count - 1;
            var exist = false;
            while (node != null)
            {
                if (node.Value == itemID)
                {
                    itemIndex++;
                    exist = true;
                    break;
                }
                itemIndex--;
                node = node.Previous;
            }

            if (!exist)
            {
                node = _items.Last;
                itemIndex = _items.Count;
            }

            return itemIndex;
        }
        
        public void AddItem(int itemID)
        {
            Assert.IsFalse(IsFull());

            var itemIndex = LookAHeadIndex(itemID, out var node);

            if (node != null)
            {
                _items.AddAfter(node, new LinkedListNode<int>(itemID));
            }
            else
            {
                _items.AddLast(new LinkedListNode<int>(itemID));
            }
            
            OnItemAddedEvent?.Invoke(itemID, itemIndex);
        }
        
        public void MergeItems(int itemID)
        {
            if (_items.Count == 0)
            {
                return;
            }
            
            var node = _items.First;
            bool found = false;
            var itemIndex = 0;
            while (node != null)
            {
                if (node.Value == itemID)
                {
                    found = true;
                    break;
                }

                node = node.Next;
                itemIndex++;
            }

            if (!found)
            {
                return;
            }

            var middleNode = node.Next;
            Assert.AreEqual(middleNode.Next.Value, itemID);
            Assert.AreEqual(middleNode.Previous.Value, itemID);
            _items.Remove(middleNode.Previous);
            _items.Remove(middleNode.Next);
            _items.Remove(middleNode);
            
            var middleItemIndex = itemIndex + 1;
            OnItemsMergedEvent?.Invoke(middleItemIndex);
        }

        public bool IsFull()
        {
            return _items.Count >= Constants.InventoryCapacity;
        }
    }
}