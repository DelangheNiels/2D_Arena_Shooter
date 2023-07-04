using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthBarComponent : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private Image _healthImage;
        [SerializeField] List<GameObject> _objectsToHide = new List<GameObject>();
        [SerializeField] private float _timeVisible = 5.0f;

        bool _isVisible = false;
        private float _visibleTimer = 0.0f;

        void Start()
        {
            if( _healthComponent != null )
                _healthComponent.OnHealthChanged += HandleHealthChanged;

            foreach (GameObject obj in _objectsToHide)
                obj.SetActive(false);
        }

        private void Update()
        {
            if(_isVisible)
            {
                _visibleTimer += Time.deltaTime;
                if(_visibleTimer >= _timeVisible)
                {
                    _isVisible = false;
                    _visibleTimer = 0.0f;

                    foreach (GameObject obj in _objectsToHide)
                        obj.SetActive(false);
                }
            }
        }

        private void OnDestroy()
        {
            _healthComponent.OnHealthChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged()
        {
            _healthImage.fillAmount = _healthComponent.GetHealthPercentage();
            _isVisible = true;
            _visibleTimer = 0.0f;
            
            foreach(GameObject obj in _objectsToHide)
                obj.SetActive(true);
        }
    }
}

