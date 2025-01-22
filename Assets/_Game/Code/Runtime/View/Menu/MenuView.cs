using System;
using UnityEngine;

namespace MatchHotel.View
{
    public class MenuView : MonoBehaviour
    {
        public virtual void Open(Action<MenuView> onCompleted)
        {
            gameObject.SetActive(true);
            onCompleted?.Invoke(this);
        }
        
        public virtual void Close(Action<MenuView> onCompleted)
        {
            gameObject.SetActive(false);
            onCompleted?.Invoke(this);
        }
    }
}