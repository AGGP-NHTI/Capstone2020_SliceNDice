using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementSpeed = 5f;



    private PlayerControls controls2 = null;


    private void Awake()
    {
        controls2 = new PlayerControls();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }
    private void OnEnable()
    {
        controls2.player2.Enable();
    }

    private void OnDisable()
    {
        controls2.player2.Disable();
    }
    private void Update()
    {

    }

    public void OnMovement()
    {
        Vector3 movementInput = controls2.player2.Movement.ReadValue<Vector2>();
        Vector3 movement = new Vector3
        {
            x = movementInput.x,
            z = movementInput.y
        }.normalized;

        transform.Translate(movement * movementSpeed * Time.deltaTime);

    }


}
