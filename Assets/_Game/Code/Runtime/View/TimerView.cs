using System;
using TMPro;
using UnityEngine;

namespace MatchHotel.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;

        public void UpdateView(TimeSpan remainingSeconds)
        {
            label.text = remainingSeconds.ToString(@"mm\:ss");
        }
    }
}