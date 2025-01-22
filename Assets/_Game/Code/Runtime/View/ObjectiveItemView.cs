using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MatchHotel.View
{
    public class ObjectiveItemView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text label;

        public void UpdateView(Sprite sprite, int count)
        {
            image.sprite = sprite;
            UpdateView(count);
        }
        
        public void UpdateView(int count)
        {
            label.text = count.ToString();
        }
    }
}