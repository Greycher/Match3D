using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MatchHotel.View
{
    public class GameplayMenu : MenuView
    {
        [SerializeField] private GameObject levelSuccessPopup;
        [SerializeField] private GameObject levelFailPopup;
        [SerializeField] private Button[] returnToLobbyButtons;
        [SerializeField] private Button retryButton;

        public UnityEvent OnRetryRequestedEvent => retryButton.onClick;
        public UnityEvent OnReturnToLobbyRequestedEvent = new();

        private void OnEnable()
        {
            foreach (var button in returnToLobbyButtons)
            {
                button.onClick.AddListener(OnReturnToLobbyButtonClicked);
            }
        }

        private void OnReturnToLobbyButtonClicked()
        {
            OnReturnToLobbyRequestedEvent?.Invoke();
        }

        private void OnDisable()
        {
            foreach (var button in returnToLobbyButtons)
            {
                button.onClick.RemoveListener(OnReturnToLobbyButtonClicked);
            }
        }

        public void OpenFailPopup()
        {
            levelFailPopup.gameObject.SetActive(true);
        }
        
        public void OpenSuccessPopup()
        {
            levelSuccessPopup.gameObject.SetActive(true);
        }
    }
}