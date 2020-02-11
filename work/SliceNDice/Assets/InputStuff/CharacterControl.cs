using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementSpeed = 10f;
    Vector2 Moveet;
    Animator anim;
    bool ismove = false;
    private PlayerControls controls = null;


    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    { 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    private void Update()
    {
        Move();
        Vector3 vel = rb.velocity;
        if(vel.magnitude == 0)
        {
            anim.SetInteger("Condition", 0);
        }
        else
        {
            anim.SetInteger("Condition", 1);
        }
        
            
        
        
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
        Vector3 nomove = Vector3.zero;
        Vector3 movement = new Vector3(Moveet.x, 0, Moveet.y) * movementSpeed * Time.deltaTime;  
        transform.Translate(movement);
   
    }

    private void OnMove(InputValue value)
    {
        Moveet = value.Get<Vector2>();
    }

   


   
}
