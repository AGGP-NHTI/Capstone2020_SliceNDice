﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementSpeed = 5f;

    

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
    private void OnEnable()
    {
        controls.Player1.Enable();
    }

    private void OnDisable()
    {
        controls.Player1.Disable();
    }
    private void Update()
    {
        
    }

    public void OnMovement()
    {
        Vector3 movementInput = controls.Player1.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        }.normalized;

        transform.Translate(movement * movementSpeed * Time.deltaTime);
       
    }

   
}
