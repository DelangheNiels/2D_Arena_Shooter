using Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class BossHealthbar : MonoBehaviour
    {
        private HealthComponent _healthComponent;
        [SerializeField] private TMP_Text _bossNameObject;
        [SerializeField] private Image _healthImage;
        [SerializeField] private GameObject _UISpawnLocation;
        [SerializeField] private List<GameObject> _objectsToSetActive = new List<GameObject>();

        private void Awake()
        {
            transform.SetParent(_UISpawnLocation.transform);
        }
       
        private void OnDestroy()
        {
            if(_healthComponent != null)
                _healthComponent.OnHealthChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged()
        {
            _healthImage.fillAmount = _healthComponent.GetHealthPercentage();
        }

        public void Initialize(string name, HealthComponent healthComp)
        {
            foreach(var obj in _objectsToSetActive)
            {
                obj.SetActive(true);
            }
            
            _bossNameObject.text = name;
            _healthComponent = healthComp;
            _healthComponent.OnHealthChanged += HandleHealthChanged;
        }
    }
}

