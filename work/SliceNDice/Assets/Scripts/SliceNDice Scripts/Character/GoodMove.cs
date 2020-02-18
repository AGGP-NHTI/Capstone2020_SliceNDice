using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace SND.BaseControls
{
    public class GoodMove : MonoBehaviour
    {
        [Header("Movement")]
        Rigidbody rb;

        public bool isRunning;
        //[Range(1f, 20f)]
        public float movementSpeed;
        //[Range(100f, 1000f)]


        private float originalMovementSpeed;







        public void Start()
        {
            // Assigning GameObjects/Components
            rb = GetComponent<Rigidbody>();



            // Assigning Variables

            originalMovementSpeed = movementSpeed;
        }



        public void MovementControls()
        {
            // Movement Control
            float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            transform.Translate(horizontal, 0, 0);

            float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
            transform.Translate(0, 0, vertical);

            // Jump Control


            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                movementSpeed = movementSpeed * 2;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                movementSpeed = originalMovementSpeed;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                gameObject.transform.Rotate(0, 2 * movementSpeed, 0);
            }

            if (Input.GetKey(KeyCode.E))
            {
                gameObject.transform.Rotate(0, -2 * movementSpeed, 0);
            }

            if (Input.GetKey(KeyCode.C))
            {
                gameObject.GetComponent<CapsuleCollider>().height = 1;
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                gameObject.GetComponent<CapsuleCollider>().height = 2;
            }
        }




    }
}
