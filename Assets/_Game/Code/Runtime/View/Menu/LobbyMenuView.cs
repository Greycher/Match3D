using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MatchHotel.View
{
    public class LobbyMenuView : MenuView
    {
        [SerializeField] private Button playButton;

        public UnityEvent OnPlayRequestedEvent => playButton.onClick;
    }
}