using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character2Control : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float movementSpeed = 5f;
    Vector2 Moveet;
    Animator anim;
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

        
    }
    private void OnEnable()
    {
        controls.Player2.Enable();
        controls.Player2.StabAttack2.performed += _ => StabAttack();
    }

    private void OnDisable()
    {
        controls.Player2.Disable();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(Moveet.x, 0, Moveet.y) * movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (movement.magnitude == 0)
        {
            anim.SetInteger("WalkCondition", 0);
        }
        else if (movement.magnitude > 0 || movement.magnitude < 0)
        {
            anim.SetInteger("WalkCondition", 1);
        }
    }

    public void StabAttack()
    {
        Debug.Log("Stab2");
        anim.SetTrigger("StabbyStab2");
       // Invoke("SetBoolBack", 1);

    }

    private void SetBoolBack()
    {
        anim.ResetTrigger("StabbyStab2");
    }



    private void OnMove(InputValue value)
    {
        Moveet = value.Get<Vector2>();
    }
}
