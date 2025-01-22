using System.Collections.Generic;
using UnityEngine;

namespace MatchHotel.View
{
    public class ObjectivesView : MonoBehaviour
    {
        [SerializeField] private ObjectiveItemView objectiveItemTemplate;

        private Dictionary<int, ObjectiveItemView> _itemIDToViewDic = new();

        private void Awake()
        {
            objectiveItemTemplate.gameObject.SetActive(false);
        }

        public void UpdateObjective(int id, int count)
        {
            _itemIDToViewDic[id].UpdateView(count);
        }

        public void AddObjective(int id, Sprite sprite, int count)
        {
            var view = Instantiate(objectiveItemTemplate, objectiveItemTemplate.transform.parent);
            view.gameObject.SetActive(true);
            view.UpdateView(sprite, count);
            _itemIDToViewDic.Add(id, view);
        }
    }
}