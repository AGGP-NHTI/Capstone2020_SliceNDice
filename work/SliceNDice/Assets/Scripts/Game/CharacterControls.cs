using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace SND.BaseControls
{
    public class CharacterControls : MonoBehaviour
    {
        [Header("Movement")]
        Rigidbody rb;
        public bool isGrounded;
        public bool isRunning;
        //[Range(1f, 20f)]
        public float movementSpeed;
        //[Range(100f, 1000f)]
        public int JumpForce;

        private float originalMovementSpeed;

        [Header("External Objects")]
        public GameObject knife;
        public GameObject camPos;
        public GameObject c;


        public void Start()
        {
            // Assigning GameObjects/Components
            rb = GetComponent<Rigidbody>();
            c = GameObject.Find("Main Camera");
            knife = GameObject.Find("Knife");
            camPos = GameObject.Find("CamPos");

            // Assigning Variables
            c.transform.position = camPos.transform.position;
            originalMovementSpeed = movementSpeed;
        }


        public void AttackControls()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                knife.SetActive(true);
            }
            else
            {
                knife.SetActive(false);
            }

        }

        public void MovementControls()
        {
            // Movement Control
            float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            transform.Translate(horizontal, 0, 0);

            float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
            transform.Translate(0, 0, vertical);

            // Jump Control
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(JumpForce);
            }

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

        void Jump(int jumpForce)
        {
            if (isGrounded)
            {
                rb.AddForce(0, jumpForce, 0);
                isGrounded = false;
            }
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.collider.tag == "Ground")
            {
                isGrounded = true;
            }
        }
    }
}