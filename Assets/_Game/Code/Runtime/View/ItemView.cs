using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace MatchHotel.View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Rigidbody body;

        private Queue<Tween> _tweenQueue = new();
        private int _itemID;
        private bool _tweenPlaying;

        public int ItemID => _itemID;

        public void InjectItemID(int itemID)
        {
            _itemID = itemID;
        }
        
        public void GoDynamic()
        {
            body.isKinematic = false;
        }

        public void GoKinematic()
        {
            body.isKinematic = true;
        }

        private void OnValidate()
        {
            if (!body)
            {
                body = GetComponent<Rigidbody>();
            }
        }

        public void PlayTweenQueued(Tween tween)
        {
            tween.Pause();
            _tweenQueue.Enqueue(tween);
            
            if (!_tweenPlaying)
            {
                ConsumeNextTweenInQueue();
            }
        }

        private void ConsumeNextTweenInQueue()
        {
            if (_tweenQueue.Count == 0)
            {
                return;
            }
            
            var tween = _tweenQueue.Dequeue();
            tween.onComplete += OnTweenComplete;
            _tweenPlaying = true;
            tween.Play();
        }

        private void OnTweenComplete()
        {
            _tweenPlaying = false;
            ConsumeNextTweenInQueue();
        }
    }
}