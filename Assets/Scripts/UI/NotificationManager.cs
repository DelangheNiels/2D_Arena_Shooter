using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField] private Notification _notificationPrefab;
        [SerializeField] private GameObject _notificationContainer;

        private static GameObject _staticNotificationContainer;
        private static Notification _staticNotificationPrefab;

        void Start()
        {
            _staticNotificationPrefab = _notificationPrefab;
            _staticNotificationContainer = _notificationContainer;
        }

        public static void CreateNotification(string text, float timeVisible)
        {
            var notification = Instantiate(_staticNotificationPrefab, _staticNotificationContainer.transform);
            notification.Initialize(text, timeVisible);
            
            notification.transform.localPosition = _staticNotificationContainer.transform.position;
        }
    }
}

