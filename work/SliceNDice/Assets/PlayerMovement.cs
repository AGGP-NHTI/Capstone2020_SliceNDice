using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private void Awake()
    {


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    [SerializeField] private float movementSpeed = 5f;
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3();
        if(Keyboard.current.wKey.isPressed)
        {
            movement.z += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            movement.z -= 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            movement.x -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            movement.x += 1;
        }
        movement.Normalize();

        transform.Translate(movement * movementSpeed * Time.deltaTime);


    }


}