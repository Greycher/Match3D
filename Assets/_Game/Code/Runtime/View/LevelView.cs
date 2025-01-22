using MatchHotel.Presenter;
using UnityEngine;

namespace MatchHotel.View
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private PileView pileView;
        [SerializeField] private ObjectivesView objectivesView;
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private ItemCollectView itemCollectView;
        [SerializeField] private TimerView timerView;
        [SerializeField] private GameplayMenu gameplayMenu;
        
        public PileView PileView => pileView;
        public ObjectivesView ObjectivesView => objectivesView;
        public InventoryView InventoryView => inventoryView;
        public ItemCollectView ItemCollectView => itemCollectView;
        public TimerView TimerView => timerView;
        public GameplayMenu GameplayMenu => gameplayMenu;

        public void OnLevelSucceed()
        {
            Debug.Log("Level succeed.");
            gameplayMenu.OpenSuccessPopup();
        }
        
        public void OnLevelFailed()
        {
            Debug.Log("Level failed.");
            gameplayMenu.OpenFailPopup();
        }
    }
}