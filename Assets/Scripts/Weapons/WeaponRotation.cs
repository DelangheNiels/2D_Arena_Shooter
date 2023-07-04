using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class WeaponRotation : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            LookAtMouse();
        }

        private void LookAtMouse()
        {
            // direction between mouse and game object
            Vector3 direction = (Vector3)Mouse.current.position.ReadValue() - Camera.main.WorldToScreenPoint(transform.position);

            //Angle between mouse and game object in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //Flip rotation so that weapon is not upside down
            if(angle < -90 || angle > 90)
                transform.rotation = Quaternion.Euler(transform.rotation.x + 180, transform.rotation.y, angle * -1);
            else
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);
        }
    }
}

