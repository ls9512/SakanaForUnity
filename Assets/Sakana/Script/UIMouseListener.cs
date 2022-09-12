using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Aya.Sakana
{
    public class UIMouseListener : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Action<PointerEventData> OnDown;
        public Action<PointerEventData> OnUp;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDown?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUp?.Invoke(eventData);
        }
    }
}
