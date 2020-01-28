using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementSpeed = 10f;
    Vector2 Moveet;
    

    private PlayerControls controls = null;


    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    private void Update()
    {
        Move();
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
    
    private void Move()
    {
        Vector3 movement = new Vector3(Moveet.x, 0, Moveet.y) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnMove(InputValue value)
    {
        Moveet = value.Get<Vector2>();
    }

   

    //public void OnMovement()
    //{
    //    Vector3 movementInput = controls.Player.Movement.ReadValue<Vector2>();
    //    Vector3 movement = new Vector3
    //    {
    //        x = movementInput.x,
    //        z = movementInput.y
    //    }.normalized;

    //    transform.Translate(movement * movementSpeed * Time.deltaTime);
       
    //}

   
}
