using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BzKovSoft.ObjectSlicerSamples;

namespace SnD.Movement
{
    public class RootMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float TurnSpeed = 2.0f;
        public float MoveSpeed = 4.0f;

        Vector3 move;

        void Update()
        {
            move = Vector3.zero;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;


            if (Input.GetKey(KeyCode.W))
            {
                move += MoveSpeed / 100f * Vector3.forward;
            }

            if (Input.GetKey(KeyCode.S))
            {
                move += MoveSpeed / 100f * Vector3.back;
            }

            if (Input.GetKey(KeyCode.A))
            {
                move += MoveSpeed / 100f * Vector3.left;
            }

            if (Input.GetKey(KeyCode.D))
            {
                move += MoveSpeed / 100f * Vector3.right;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                gameObject.transform.Rotate(0, TurnSpeed, 0);
            }

            if (Input.GetKey(KeyCode.E))
            {
                gameObject.transform.Rotate(0, -TurnSpeed, 0);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                move *= 2;
            }

            gameObject.transform.Translate(move, Space.Self);          
        }
    }
}