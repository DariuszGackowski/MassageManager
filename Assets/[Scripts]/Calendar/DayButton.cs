using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MassageApp.Calendar
{
    public class DayButton : MonoBehaviour
    {
        public Image DayImageBackground;
        public Image EventIndicatorImage;
        public TextMeshProUGUI DayText;
        public Color CurrentDayTextColor;
        public Color CustomDayTextColor;
        public Color DayImageCustomColor;
        public Color DayImageCheckedColor;

        private int _day;
        public void Initialize(int dayNumber, bool isCurrentDay)
        {
            _day = dayNumber;
            DayText.SetText(dayNumber.ToString());

            DayText.color = CustomDayTextColor;
            DayImageBackground.enabled = false;
            EventIndicatorImage.enabled = false;

            if (isCurrentDay)
            {
                SetAsCurrentDay();
            }
        }
        public void SetAsCurrentDay()
        {
            DayImageBackground.enabled = true;
            DayImageBackground.color = DayImageCustomColor;
            DayText.color = CurrentDayTextColor;
        }

        public void SetEvents(int eventCount)
        {
            EventIndicatorImage.enabled = eventCount > 0;
        }
        public void SetCheckedDay()
        {
            DayImageBackground.color = DayImageCheckedColor;
            DayImageBackground.enabled = true;
        }
    }
}