using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class DamageNumber : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damageTextObject;
        [SerializeField] private float _timeToLive = 0.5f;
        [SerializeField] private float _movementSpeed = 0.5f;

        private float _timer = 0.0f;

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= _timeToLive)
                Destroy(gameObject);

            transform.position += new Vector3(0,_movementSpeed * Time.deltaTime, 0);
        }

        public void SetDamage(int damage)
        {
            _damageTextObject.text = damage.ToString();
        }
    }
}

