using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class FaceMouseDirection : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            Vector2 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);

            if (direction.x < 0)
                transform.rotation = Quaternion.Euler(0, 180.0f, 0);
            else
                transform.rotation = Quaternion.Euler(0, 0.0f, 0);
        }
    }
}

