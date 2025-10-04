using UnityEngine;
using System;
using System.Globalization;
using System.Collections.Generic;
using TMPro;
using MassageApp.Database;
using MassageApp.Core;

namespace MassageApp.Calendar
{
    public class CalendarManager : MonoBehaviour
    {
        public DatabaseManager DatabaseManager;
        public List<DayButton> DayButtons = new List<DayButton>();
        public TextMeshProUGUI MonthAndYearText;

        private DateTime _currentDate;
        private const int _maxDayButtons = 42;

        private void Awake()
        {
            if (DayButtons.Count != _maxDayButtons)
            {
                Debug.LogError($"CalendarManager reference list must contain exactly {_maxDayButtons} DayButtons. Found: {DayButtons.Count}");
            }
        }

        private void Start()
        {
            _currentDate = DateTime.Today;
            GenerateCalendar(_currentDate.Year, _currentDate.Month);
        }

        public void NextMonth()
        {
            _currentDate = _currentDate.AddMonths(1);
            GenerateCalendar(_currentDate.Year, _currentDate.Month);
        }

        public void PreviousMonth()
        {
            _currentDate = _currentDate.AddMonths(-1);
            GenerateCalendar(_currentDate.Year, _currentDate.Month);
        }

        private void GenerateCalendar(int year, int month)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            string formattedDate = firstDayOfMonth.ToString("MMMM yyyy", CultureInfo.CurrentCulture);
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string capitalizedDate = textInfo.ToTitleCase(formattedDate);
            MonthAndYearText.SetText(capitalizedDate);

            List<Appointment> appointments = DatabaseManager.GetAppointmentsForMonth(year, month);
            Dictionary<int, int> appointmentMap = new Dictionary<int, int>();
            foreach (Appointment appointment in appointments)
            {
                if (DateTime.TryParse(appointment.DateTime, out DateTime appDate))
                {
                    int day = appDate.Day;
                    if (appointmentMap.ContainsKey(day))
                    {
                        appointmentMap[day]++;
                    }
                    else
                    {
                        appointmentMap.Add(day, 1);
                    }
                }
            }
            
            int dayOfWeekIndex = (int)firstDayOfMonth.DayOfWeek;
            Debug.Log($"First day of month {firstDayOfMonth.ToShortDateString()} is a {firstDayOfMonth.DayOfWeek} with index {dayOfWeekIndex}");
            dayOfWeekIndex = (dayOfWeekIndex == 0) ? 6 : dayOfWeekIndex - 1;

            int daysInMonth = DateTime.DaysInMonth(year, month);
            int buttonIndex = 0;
            for (int i = 0; i < dayOfWeekIndex; i++)
            {
                DayButtons[buttonIndex].Disable();
                buttonIndex++;
            }

            for (int day = 1; day <= daysInMonth; day++)
            {
                DayButton dayButton = DayButtons[buttonIndex];
                dayButton.gameObject.SetActive(true);

                bool isToday = (year == DateTime.Today.Year && month == DateTime.Today.Month && day == DateTime.Today.Day);
                dayButton.Initialize(day, isToday);

                if (appointmentMap.ContainsKey(day))
                {
                    int count = appointmentMap[day];
                    dayButton.SetEvents(count);
                }

                buttonIndex++;
            }

            for (int i = buttonIndex; i < _maxDayButtons; i++)
            {
                DayButtons[i].gameObject.SetActive(false);
            }
        }
    }
}