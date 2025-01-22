using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MatchHotel.View;

namespace MatchHotel.Presenter
{
    public class TimerPresenter
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly TimerView _timerView;
        private readonly TimeSpan _timeSpan;
        private readonly Action _timerExpiredAction;

        public TimerPresenter(int seconds, TimerView timerView, Action timerExpiredAction)
        {
            _timerView = timerView;
            _timerExpiredAction = timerExpiredAction;
            _cancellationTokenSource = new CancellationTokenSource();
            _timeSpan = TimeSpan.FromSeconds(seconds);
            _timerView.UpdateView(_timeSpan);
        }

        public void StartTimer()
        {
            UpdateViewAsync(_timeSpan, _cancellationTokenSource.Token);
        }

        private async void UpdateViewAsync(TimeSpan span, CancellationToken cancellationToken)
        {
            var second = TimeSpan.FromSeconds(1);
            while (span.TotalSeconds > 0)
            {
                await UniTask.Delay(second, cancellationToken: cancellationToken)
                    .SuppressCancellationThrow();
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                span = span.Subtract(second);
                _timerView.UpdateView(span);
            }
            
            _timerExpiredAction?.Invoke();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}