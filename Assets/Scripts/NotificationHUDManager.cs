using System.Collections.Generic;
using UnityEngine;

public class NotificationHUDManager : MonoBehaviour
{
    public RectTransform notificationsPanel;

    [SerializeField] private GameObject _notificationPrefab;

    private void Awake()
    {
        EventManager.AddListener<NotifyPlayerEvent>(OnNotifyPlayer);
    }

    private void OnNotifyPlayer(NotifyPlayerEvent notification)
    {
        Debug.Log("Handling notification");

        GameObject notificationUIInstance = Instantiate(_notificationPrefab, notificationsPanel);

        notificationUIInstance.transform.SetSiblingIndex(0);

        NotificationToast toast = notificationUIInstance.GetComponent<NotificationToast>();

        toast.Initialise(notification.titleText, notification.descriptionText, notification.delayVisible);

        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(notificationsPanel);
    }
}
