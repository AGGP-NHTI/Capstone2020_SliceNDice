using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementSpeed = 5f;
    Vector2 Moveet;
    Animator anim;
    bool ismove = false;
    private PlayerControls controls = null;
    float speed;

    private IEnumerator coroutine;

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

        controls.Player.StabAttack.performed += _ => StabAttack();
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

        if (movement.magnitude == 0)
        {
            anim.SetInteger("Condition", 0);
        }
        else if (movement.magnitude > 0 || movement.magnitude < 0)
        {
            anim.SetInteger("Condition", 1);
        }
    }

    public void StabAttack()
    {
        anim.SetTrigger("StabbyStab");
        Invoke("SetBoolBack", 1);

    }

    private void SetBoolBack()
    {
        anim.ResetTrigger("StabbyStab");
    }



    private void OnMove(InputValue value)
    {
        Moveet = value.Get<Vector2>();
    } 
}
