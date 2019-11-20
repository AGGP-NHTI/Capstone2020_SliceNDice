using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private float JumpHeight = 3f;

    private PlayerControls controls = null;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 movementInput = controls.Player.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        }.normalized;

        transform.Translate(movement * movementSpeed * Time.deltaTime);

    }

    public void Jump()
    {
        transform.position = new Vector3(transform.position.x,
            transform.position.y + JumpHeight, transform.position.z);


    }
}
