using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class FacePlayer : MonoBehaviour
    {
        private PlayerManager _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerManager>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 direction = _player.transform.position - transform.position;

            if (direction.x < 0)
                transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0.0f, 0);
        }
    }
}

