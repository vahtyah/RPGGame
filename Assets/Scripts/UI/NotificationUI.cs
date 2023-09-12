using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public enum NotificationType
    {
        Default,
        Craft,
        PickUp,
        Skill
    }

    public class NotificationUI : MonoBehaviour
    {
        public static NotificationUI Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI notificationText;

        public Queue<string> notiQueue;
        private RectTransform rectTransform;
        private NotificationType currentType;

        private bool isAppear;
        private bool isFinish;
        private Vector3 destinationPosition;
        private float interpolate;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            destinationPosition = new Vector3(-rectTransform.rect.width, 0);
            notiQueue = new Queue<string>();
            Setup();
        }

        private void Update()
        {
            MoveTextHandle();
            NotificationHandle();
        }

        private void NotificationHandle()
        {
            var distance = Vector2.Distance(notificationText.rectTransform.anchoredPosition, destinationPosition);
            if (distance < 20f && !isAppear)
            {
                isAppear = true;
                StartCoroutine(EndAppear());
            }

            if (distance < 20f && isFinish && isAppear)
            {
                isFinish = false;
                Setup();
            }
        }

        private void MoveTextHandle()
        {
            if (!isAppear)
                notificationText.rectTransform.anchoredPosition = Vector3.Lerp(
                    notificationText.rectTransform.anchoredPosition, destinationPosition,
                    2 * Time.deltaTime);
            else
            {
                notificationText.rectTransform.anchoredPosition =
                    Vector2.MoveTowards(notificationText.rectTransform.anchoredPosition, destinationPosition, 3);
            }
        }

        private IEnumerator EndAppear()
        {
            yield return new WaitForSeconds(2f);
            destinationPosition -= new Vector3(notificationText.rectTransform.rect.width, 0);
            isFinish = true;
            currentType = default;
        }

        private void Setup(NotificationType type = default)
        {
            currentType = type;
            destinationPosition = new Vector3(-rectTransform.rect.width, 0);
            notificationText.rectTransform.anchoredPosition = Vector2.zero;
            isFinish = false;
            isAppear = false;
            if (notiQueue.Count > 0)
                notificationText.text = notiQueue.Dequeue();
            else gameObject.SetActive(false);
        }

        public void AddNotification(string text, NotificationType type, string name = "")
        {
            if (gameObject.activeSelf && currentType == type)
            {
                UpdateText(name);
                return;
            }

            notiQueue.Enqueue(text);
            if (gameObject.activeSelf) return;
            Setup(type);
            gameObject.SetActive(true);
        }

        public void UpdateText(string text) { notificationText.text += text; }
    }
}