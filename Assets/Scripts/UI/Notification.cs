using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textObject;
        [SerializeField] private float _movementSpeed = 5.0f;

        private float _timeVisible;
        private float _timer;
        private bool _isVisible;

        private RectTransform _rectTransform;
        private float _moveDistanceX;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _moveDistanceX = _rectTransform.sizeDelta.x;
        }

        void Update()
        {
            if (!_isVisible)
                return;

            _timer +=Time.deltaTime;
            if(_timer > _timeVisible)
            {
                _timer = 0;
                _isVisible = false;
                Hide();
            }
        }

        public void Initialize(string text, float timeVisible)
        {
            _textObject.text = text;
            _timeVisible = timeVisible;
            _isVisible = true;
            StartCoroutine(CoShow());
        }

        private IEnumerator CoShow()
        {
            float movedDistance = 0;

            while(movedDistance < _moveDistanceX)
            {
                float distance = _movementSpeed * Time.deltaTime;
                transform.localPosition += new Vector3(distance, 0, 0);
                movedDistance += distance;

                if(movedDistance > _moveDistanceX)
                {
                    distance = movedDistance - _moveDistanceX;
                    transform.localPosition -= new Vector3(distance, 0, 0);
                }
            }

            yield return null;
        }

        private void Hide()
        {
            Destroy(gameObject);
        }
    }
}

