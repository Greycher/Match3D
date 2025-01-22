using System;
using MatchHotel.Presenter;
using TMPro;
using UnityEngine;

namespace MatchHotel.View
{
    public class LevelNumberView : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private string format = "Level {0}";
        
        private LevelNumberPresenter _presenter;

        private void Awake()
        {
            _presenter = new LevelNumberPresenter(this);
        }

        public void UpdateView(int levelNumber)
        {
            label.text = String.Format(format, levelNumber.ToString());
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}