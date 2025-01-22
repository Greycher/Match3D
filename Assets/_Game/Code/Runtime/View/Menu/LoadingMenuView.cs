using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MatchHotel.View
{
    public class LoadingMenuView : MenuView
    {
        [SerializeField] private float minLoadingDur = 2f;
        
        private float _openTime;

        public override void Open(Action<MenuView> onCompleted)
        {
            _openTime = Time.realtimeSinceStartup;
            base.Open(onCompleted);
        }

        public override async void Close(Action<MenuView> onCompleted)
        {
            var elapsedTime = _openTime + minLoadingDur - Time.realtimeSinceStartup;
            if (elapsedTime > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(elapsedTime),
                    cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            base.Close(onCompleted);
        }
    }
}