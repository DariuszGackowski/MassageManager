using UnityEngine;
using UnityEngine.EventSystems;

namespace MassageApp.Calendar
{
    public class SwipeHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CalendarManager CalendarManager;
        private Vector2 _dragStartPos;
        private const float _swipeThreshold = 50f;
        void Start()
        {
            if (CalendarManager == null)
            {
                Debug.LogError("CalendarManager reference is not assigned in SwipeHandler!");
            }
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragStartPos = eventData.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            // Optional drag logic (e.g., visual offset)  
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            Vector2 dragEndPos = eventData.position;
            float xDiff = dragEndPos.x - _dragStartPos.x;

            if (Mathf.Abs(xDiff) > _swipeThreshold)
            {
                if (xDiff > 0)
                {
                    CalendarManager.PreviousMonth();
                }
                else
                {
                    CalendarManager.NextMonth();
                }
            }
        }
    }
}